using Sesc.MeuSesc.SharedKernel.Infrastructure.Entities;
using System;

namespace Sesc.Domain.Habilitacao.Entities
{
    public class InformacaoProfissional : EntityBase
    {
        public string CNPJ { get; set; }
        public string NomeEmpresa { get; set; }
        public Nullable<DateTime> DataAdmissao { get; set; }
        public Nullable<DateTime> DataDemissao { get; set; }
        public string Ocupacao { get; set; }
        public string NumeroCTPS { get; set; }
        public string SerieCTPS { get; set; }
        public decimal Renda { get; set; }

        public void ChangeInformacaoProfissional(InformacaoProfissional informacaoProfissionalNew)
        {
            if (informacaoProfissionalNew == null) return;

            this.CNPJ = informacaoProfissionalNew.CNPJ;
            this.NomeEmpresa = informacaoProfissionalNew.NomeEmpresa;
            this.DataAdmissao = informacaoProfissionalNew.DataAdmissao;
            this.DataDemissao = informacaoProfissionalNew.DataDemissao;
            this.Ocupacao = informacaoProfissionalNew.Ocupacao;
            this.NumeroCTPS = informacaoProfissionalNew.NumeroCTPS;
            this.SerieCTPS = informacaoProfissionalNew.SerieCTPS;
            this.Renda = informacaoProfissionalNew.Renda;
        }
    }
}
