using System.Threading.Tasks;
using Auditing;
using AutoMapper;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Sesc.Application.ApplicationServices.Commands;
using Sesc.Application.ApplicationServices.Commands.Contracts;
using Sesc.Application.ApplicationServices.Queries;
using Sesc.Application.ApplicationServices.Queries.Contracts;
using Sesc.CrossCutting.Config;
using Sesc.CrossCutting.Notification.Config;
using Sesc.CrossCutting.Notification.IoC;
using Sesc.CrossCutting.ServiceAgents.AuthServer.Services;
using Sesc.CrossCutting.ServiceAgents.AuthServer.Services.Contracts;
using Sesc.CrossCutting.ServiceAgents.Clientela;
using Sesc.CrossCutting.ServiceAgents.Clientela.Contracts;
using Sesc.CrossCutting.ServiceAgents.Clientela.Services;
using Sesc.CrossCutting.ServiceAgents.Clientela.Services.Contracts;
using Sesc.CrossCutting.ServiceAgents.Jasper.Config;
using Sesc.CrossCutting.ServiceAgents.Jasper.Repository;
using Sesc.CrossCutting.ServiceAgents.Sharepoint.Config;
using Sesc.CrossCutting.ServiceAgents.Sharepoint.IoC;
using Sesc.Domain.Habilitacao.Dto;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.Domain.Habilitacao.Repositories.Contracts;
using Sesc.Domain.Habilitacao.Services;
using Sesc.Domain.Habilitacao.Services.Contracts;
using Sesc.MeuSesc.Api.Habilitacao.Base.Filters;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Config;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Context;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Persistence.Repositories;
using Sesc.MeuSesc.Infrastructure.EntityFramework.UnitOfWork;
using Sesc.MeuSesc.SharedKernel.Authentication.Bootstrap;
using Sesc.MeuSesc.SharedKernel.Authentication.Config;
using Sesc.MeuSesc.SharedKernel.Infrastructure.DataContext;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Repositories;
using Sesc.MeuSesc.SharedKernel.Infrastructure.UnitOfWork;
using Swashbuckle.AspNetCore;

namespace Sesc.MeuSesc.Api.Habilitacao
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }
        readonly string AllowSpecificOrigins = "CorsPolicy";

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            HostingEnvironment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => {
                options.AddPolicy(AllowSpecificOrigins, 
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                );
            });

            services.Configure<ApiConfig>(Configuration.GetSection("ApiConfig"));
            services.Configure<ConnectionStringsConfig>(Configuration.GetSection("ConnectionStrings"));

            //Notification
            services.Configure<MessageBusOptions>(Configuration.GetSection("MessageBrokerServer"));
            services.Configure<EmailQueueConfig>(Configuration.GetSection("EmailMessage"));
            services.Configure<EmailServerConfig>(Configuration.GetSection("EmailServerConfig"));
            services.Configure<SharePointConfig>(Configuration.GetSection("SharePointConfig"));
            
            services.Configure<AuditManagerOptions>(Configuration.GetSection("Audit"));
            services.AddScoped<IAuditManager, AuditManager>();

            //services.AddAutoMapper(typeof(Startup));
            services.AddAutoMapper(c => c.AddProfile<AutoMapping>(), typeof(Startup));

            services.AddDbContext<SescContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("SescConnection"))
            );

            services.AddMvc()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            })
            .AddMvcOptions(options =>
            {
                options.Filters.Add(new ApiExceptionFilter());
            });

            services.AddSwaggerGen(x => x.SwaggerDoc("v1", new OpenApiInfo { Title = "Habilitação - Sesc API", Version = "v1" }));

            services.Configure<AuthClient>(Configuration.GetSection("AuthClient"));
            services.Configure<AuthServer>(Configuration.GetSection("AuthServer"));
            services.Configure<JasperConfig>(Configuration.GetSection("JasperConfig"));

            RegisterServices(services);
            ConfigureAuth(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseCors(AllowSpecificOrigins);
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // configura auditoria
            app.UseAuditManager();

            app.UseSwagger();
            app.UseSwaggerUI(option => option.SwaggerEndpoint("/swagger/v1/swagger.json", "Habilitação de Cartão - API V1"));
            app.UseWelcomePage("/swagger");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
            });

            app.Run(context =>
            {
                context.Response.Redirect("/swagger/index.html");
                return Task.CompletedTask;
            });
        }

        private void ConfigureAuth(IServiceCollection services)
        {
            var authServer = Configuration.GetSection("AuthServer").Get<AuthServer>();

            services.AddAuthentication("Bearer")
              .AddIdentityServerAuthentication(x =>
              {
                  x.Authority = authServer.Authority;
                  x.ApiSecret = authServer.ApiSecret;
                  x.ApiName = authServer.ApiName;
                  x.SupportedTokens = SupportedTokens.Jwt;
                  x.RequireHttpsMetadata = authServer.RequireHttpsMetadata;
              });

            services.AddAuthorization();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            //Services
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IPessoaService, PessoaService>();
            services.AddScoped<ISolicitacaoService, SolicitacaoService>();
            services.AddScoped<IDependenteService, DependenteService>();
            services.AddScoped<IDocumentoService, DocumentoService>();
            services.AddScoped<IAtendimentoService, AtendimentoService>();

            //Repositories
            services.AddScoped<ICidadeRepository, CidadeRepository>();
            services.AddScoped<IEstadoRepository, EstadoRepository>();
            services.AddScoped<ISolicitacaoRepository, SolicitacaoRepository>();
            services.AddScoped<IDependenteRepository, DependenteRepository>();
            services.AddScoped<IDocumentoRepository, DocumentoRepository>();
            services.AddScoped<IAtendimentoRepository, AtendimentoRepository>();
            services.AddScoped<INotificacaoTemplateRepository, NotificacaoTemplateRepository>();
            services.AddScoped<IJasperServiceRepository, JasperServiceRepository>();

            //Service application
            services.AddScoped<ICidadeQueryServiceApplication, CidadeQueryServiceApplication>();
            services.AddScoped<IEstadoQueryServiceApplication, EstadoQueryServiceApplication>();
            services.AddScoped<ISolicitacaoQueryServiceApplication, SolicitacaoQueryServiceApplication>();
            services.AddScoped<IPessoaScaQueryServiceApplication, PessoaScaQueryServiceApplication>(); 
            services.AddScoped<IDocumentoQueryServiceApplication, DocumentoQueryServiceApplication>();
            //
            services.AddScoped<IPessoaCommandServiceApplication, PessoaCommandServiceApplication>();
            services.AddScoped<IUsuarioCommandServiceApplication, UsuarioCommandServiceApplication>();
            services.AddScoped<ISolicitacaoCommandServiceApplication, SolicitacaoCommandServiceApplication>();
            services.AddScoped<IAuthCommandServiceApplication, AuthCommandServiceApplication>();
            services.AddScoped<IDependenteCommandServiceApplication, DependenteCommandServiceApplication>();
            services.AddScoped<IDocumentoCommandServiceApplication, DocumentoCommandServiceApplication>();
            services.AddScoped<IAtendimentoCommandServiceApplication, AtendimentoCommandServiceApplication>();

            //Sca
            services.AddScoped<IEnderecoScaService, EnderecoScaService>();
            services.AddScoped<IPessoaScaService, PessoaScaService>();
            services.AddScoped<IEmpresaScaService, EmpresaScaService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IDataContext>(provider => provider.GetService<SescContext>());
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ISescApiCentralAtendimento, SescApiCentralAtendimento>();

            services.AddTransient<IUserAuthenticatedAuthService, UserAuthenticatedAuthService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserAuthService, UserAuthService>();
            services.AddTransient<ITokenAuthService, TokenAuthService>();

            AuthenticationInjectorBootStrapper.RegisterServices(services);
            SharePointInjectorBootStrapper.RegisterServices(services);
            NotificationInjectorBootStrapper.RegisterServices(services);
        }
    }
}
