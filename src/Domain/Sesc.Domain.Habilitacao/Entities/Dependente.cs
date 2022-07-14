using Sesc.Domain.Habilitacao.Enum;

namespace Sesc.Domain.Habilitacao.Entities
{
    public class Dependente : Pessoa
    {  
        public ParentescoEnum Parentesco { get; set; }
        public AcaoEnum? Acao { get; set; }
        public int TitularId { get; set; }
        public Titular Titular { get; set; }
        //public RenovarEnum? Renovar { get; set; }
    }
}
