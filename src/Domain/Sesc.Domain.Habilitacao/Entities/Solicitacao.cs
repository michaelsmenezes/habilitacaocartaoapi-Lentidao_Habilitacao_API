using Sesc.Domain.Habilitacao.Enum;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sesc.Domain.Habilitacao.Entities
{
    public class Solicitacao : EntityBase
    {
        public int TitularId { get; set; }
        public Titular Titular { get; set; }
        public SolicitacaoSituacaoEnum Situacao { get; set; }
        public string Cpf { get; set; }
        public SolicitacaoTipoEnum Tipo { get; set; }
        public SolicitacaoCategoriaEnum Categoria { get; set; }
        public DateTime DataRegistro { get; set; }
        public DateTime? DataEnvio { get; set; }
        public bool EmAtendimento { get; set; }
        public SolicitacaoPlataformaEnum? Plataforma { get; set; }
        public ICollection<Atendimento> Atendimentos { get; set; }

        public void ChangeSolicitacao(Solicitacao newSolicitacao)
        {
            if (newSolicitacao == null) return;

            this.Categoria = newSolicitacao.Categoria;
        }

        public void Enviar()
        {
            DataEnvio = DateTime.Now;
        }
    }
}
