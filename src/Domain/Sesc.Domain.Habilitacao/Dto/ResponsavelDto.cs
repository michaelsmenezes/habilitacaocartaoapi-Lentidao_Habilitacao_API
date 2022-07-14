using Sesc.Domain.Habilitacao.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sesc.Domain.Habilitacao.Dto
{
    public class ResponsavelDto : PessoaDto
    {
        public ParentescoEnum? Parentesco { get; set; }
    }
}
