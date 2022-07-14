using System.Collections.Generic;

namespace Sesc.Domain.Habilitacao.Dto
{
    public class TitularDto : PessoaDto
    {
        public TitularDto()
        {
            Dependentes = new List<DependenteDto>();
        }

        public IList<DependenteDto> Dependentes { get; set; }
        public ResponsavelDto Responsavel { get; set; }
        public virtual InformacaoProfissionalDto InformacaoProfissional { get; set; }
    }
}
