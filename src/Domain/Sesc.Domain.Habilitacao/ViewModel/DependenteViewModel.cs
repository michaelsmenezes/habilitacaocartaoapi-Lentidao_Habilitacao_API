using Sesc.Domain.Habilitacao.Enum;

namespace Sesc.Domain.Habilitacao.ViewModel
{
    public class DependenteViewModel : PessoaViewModel
    {
        public ParentescoEnum? Parentesco { get; set; }
        public AcaoEnum? Acao { get; set; }
        //public RenovarEnum? Renovar { get; set; }
    }
}
