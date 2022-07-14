using Auditing;
using Auditing.EventArgs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Extensions;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Persistence.Mappings;
using Sesc.MeuSesc.SharedKernel.Infrastructure.DataContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Context
{
    public class SescContext : DbContext, IDataContext
    {
        //-- campos para auditoria
        private List<EntityEntry> _newEntitiesList;
        private List<EntityEntry> _deletedEntitiesList;
        private List<EntityEntry> _modifiedEntitiesList;
        private readonly IAuditManager _auditManager;

        public SescContext(DbContextOptions<SescContext> options,
               IAuditManager auditManager
        ) : base(options)
        {
            _auditManager = auditManager;
        }

        public SescContext()
        {

        }

        //HABILITACAO MATRICULA
        public DbSet<Atendimento> Atendimento { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Contato> Contato { get; set; }
        public DbSet<Dependente> Dependente { get; set; }
        public DbSet<Documento> Documento { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<InformacaoProfissional> InformacaoProfissional { get; set; }
        public DbSet<Responsavel> Responsavel { get; set; }
        public DbSet<Solicitacao> Solicitacao { get; set; }
        public DbSet<Titular> Titular { get; set; }
        public DbSet<NotificacaoTemplate> NotificacaoTemplate { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new AtendimentoMap());
            modelBuilder.AddConfiguration(new CidadeMap());
            modelBuilder.AddConfiguration(new ContatoMap());
            modelBuilder.AddConfiguration(new PessoaMap());
            modelBuilder.AddConfiguration(new DependenteMap());
            modelBuilder.AddConfiguration(new DocumentoMap());
            modelBuilder.AddConfiguration(new EstadoMap());
            modelBuilder.AddConfiguration(new InformacaoProfissionalMap());
            modelBuilder.AddConfiguration(new ResponsavelMap());
            modelBuilder.AddConfiguration(new SolicitacaoMap());
            modelBuilder.AddConfiguration(new TitularMap());
            modelBuilder.AddConfiguration(new NotificacaoTemplateMap());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory() + "/../../API/Sesc.MeuSesc.Api.Habilitacao")
                     .AddJsonFile("appsettings.json", false, true)
                     .Build();
                var connectionString = configuration.GetConnectionString("SescConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public override int SaveChanges()
        {
            PreAudit(); // detecta as entidades novas, excluidas e alteradas
            int r = base.SaveChanges(); // banco de dados
            PostAudit(); // confirma alterações e dispara eventos

            return r;
        }

        private void PreAudit()
        {
            ChangeTracker.DetectChanges();
            _newEntitiesList = ChangeTracker.Entries().Where(p => p.State == EntityState.Added).ToList();
            _modifiedEntitiesList = ChangeTracker.Entries().Where(p => p.State == EntityState.Modified).ToList();
            _deletedEntitiesList = ChangeTracker.Entries().Where(p => p.State == EntityState.Deleted).ToList();
        }

        private void PostAudit()
        {
            if (_auditManager != null)
            {
                RaiseAuditEntityEvent(_newEntitiesList, t => t.Action = AuditEventArgs.INSERT);
                RaiseAuditEntityEvent(_modifiedEntitiesList, t => t.Action = AuditEventArgs.UPDATE);
                RaiseAuditEntityEvent(_deletedEntitiesList, t => t.Action = AuditEventArgs.DELETE);
            }
        }

        private void RaiseAuditEntityEvent(List<EntityEntry> entradas, Action<AuditEventArgs> acao)
        {
            foreach (var item in entradas)
            {
                var eventArgs = new AuditEventArgs
                {
                    EntityNamespace = item.Entity.GetType().Namespace,
                    EntityName = item.Entity.GetType().Name,
                    EntityId = GetPrimaryKeyValue(item.Entity)
                };

                // configura a ação
                acao(eventArgs);

                var fields = GetEntityFields(item);
                eventArgs.Fields = fields;
                _auditManager.OnAuditEntity(eventArgs);
            }
        }


        private Dictionary<string, string> GetEntityFields(EntityEntry item)
        {
            var ret = new Dictionary<string, string>();
            foreach (var prop in item.Entity.GetType().GetTypeInfo().DeclaredProperties)
            {
                var propName = prop.Name;
                try
                {
                    // pode lancar exceção (property does not exist), por isso dentro do try catch 
                    var propVal = $"{item.Property(propName).CurrentValue}";
                    ret.Add(propName, propVal);
                }
                catch { }
            }
            return ret;
        }

        protected virtual int GetPrimaryKeyValue<T>(T entity)
        {
            try
            {
                var keyName = this.Model
                    .FindEntityType(entity.GetType())
                    .FindPrimaryKey()
                    .Properties
                    .Select(x => x.Name).Single();

                object keyVal = (int)entity.GetType().GetProperty(keyName).GetValue(entity, null);

                int value;
                if (int.TryParse($"{keyVal}", out value))
                {
                    return value;
                }
            }
            catch { }

            return -1;
        }
    }
}
