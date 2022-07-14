using Sesc.Domain.Habilitacao.Enum;

namespace Sesc.Domain.Habilitacao.Dto
{
    public class DependenteDto : PessoaDto
    {
        public ParentescoEnum? Parentesco { get; set; }
        public AcaoEnum? Acao { get; set; }
        //public RenovarEnum? Renovar { get; set; }
    }
}
