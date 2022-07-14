using System;

namespace Sesc.Domain.Habilitacao.ViewModel
{
    public class InformacaoProfissionalViewModel
    {
        public int? Id { get; set; }
        public string CNPJ { get; set; }
        public string NomeEmpresa { get; set; }
        public DateTime? DataAdmissao { get; set; }
        public DateTime? DataDemissao { get; set; }
        public string Ocupacao { get; set; }
        public string NumeroCTPS { get; set; }
        public string SerieCTPS { get; set; }
        public decimal? Renda { get; set; }
    }
}