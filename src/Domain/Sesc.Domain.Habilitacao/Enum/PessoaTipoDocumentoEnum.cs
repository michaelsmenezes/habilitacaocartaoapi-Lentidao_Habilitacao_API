using System.ComponentModel.DataAnnotations;

namespace Sesc.Domain.Habilitacao.Enum
{
    public enum PessoaTipoDocumentoEnum
    {
        [Display(Name = "Carteira de Identidade")]
        rg = 1,

        [Display(Name = "Carteira Nacional de Habilitação")]
        cnh = 2,

        [Display(Name = "Carteira de Registro Profissional")]
        carteiraRegistroProfissional = 3,

        [Display(Name = "Carteira de Trabalho")]
        ctps = 4,

        [Display(Name = "Certidão de Nascimento")]
        certidaoNascimento = 5,

        [Display(Name = "Certificado de Reservista")]
        certidaoReservista = 6,

        [Display(Name = "Passaporte")]
        passaporte = 7,

        [Display(Name = "Registro Nacional de Estrangeiro")]
        registroEstrangeiro = 8,
    }
}