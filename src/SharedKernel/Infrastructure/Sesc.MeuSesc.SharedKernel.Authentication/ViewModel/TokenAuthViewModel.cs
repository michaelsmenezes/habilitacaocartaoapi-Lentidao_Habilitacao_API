using System;
using System.Collections.Generic;
using System.Text;

namespace Sesc.SharedKernel.Authentication.ViewModel
{
    public class TokenAuthViewModel
    {
        public string AccessToken { get; set; }

        public long ExpiresIn { get; set; }

        public string TokenType { get; set; }

        public string RefreshToken { get; set; }

        public bool PessoaExists { get; set; }

        public bool HasCadastroValido { get; set; }

        public string foto { get; set; }
    }
}
