using Sesc.Domain.Habilitacao.Enum;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sesc.Domain.Habilitacao.Dto
{
    public class PessoaDto : DtoBase
    {
        public PessoaDto()
        {
            Documentos = new Collection<DocumentoDto>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public string NomeSocial { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string NomeMae { get; set; }
        public string NomePai { get; set; }
        public int ContatoId { get; set; }
        public ContatoDto Contato { get; set; }
        public int InformacaoProfissionalId { get; set; }
        public InformacaoProfissionalDto InformacaoProfissional { get; set; }
        public PessoaEscolaridadeEnum Escolaridade { get; set; }
        public PessoaEstadoCivilEnum EstadoCivil { get; set; }
        public string UltimaSerie { get; set; }
        public string Nacionalidade { get; set; }
        public string Naturalidade { get; set; }
        public PessoaTipoDocumentoEnum TipoDocumento { get; set; }
        public string Numero { get; set; }
        public string OrgaoEmissor { get; set; }
        public Nullable<DateTime> DataEmissao { get; set; }
        public Nullable<DateTime> DataVencimento { get; set; }
        public ICollection<DocumentoDto> Documentos { get; set; }
    }
}
