using System.Collections.Generic;

namespace Sesc.Domain.Habilitacao.ViewModel
{
    public class TitularViewModel : PessoaViewModel
    {
        public TitularViewModel()
        {
            Dependentes = new List<DependenteViewModel>();
        }

        public IList<DependenteViewModel> Dependentes { get; set; }
        public ResponsavelViewModel Responsavel { get; set; }
        public virtual InformacaoProfissionalViewModel InformacaoProfissional { get; set; }
    }
}
