using System;
using System.Collections.Generic;

namespace Sesc.Domain.Habilitacao.Dto
{
    public class SolicitacaoFiltrosDto
    {
        public SolicitacaoFiltrosDto()
        {
            Situacoes = new List<int>();
        }

        public string Page { get; set; }
        public string PageSize { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Situacao { get; set; }
        public string Tipo { get; set; }
        public string MunicipioResponsavel { get; set; }
        public IList<int> Situacoes { get; set; }
    }
}
