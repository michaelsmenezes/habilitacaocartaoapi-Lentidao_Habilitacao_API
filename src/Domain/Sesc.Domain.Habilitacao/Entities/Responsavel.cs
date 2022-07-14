using Sesc.Domain.Habilitacao.Enum;

namespace Sesc.Domain.Habilitacao.Entities
{
    public class Responsavel : Pessoa
    {
        public ParentescoEnum? Parentesco { get; set; }
    }
}
