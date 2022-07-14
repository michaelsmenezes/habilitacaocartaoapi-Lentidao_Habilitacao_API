using Sesc.SharedKernel.Authentication.ViewModel;

namespace Sesc.CrossCutting.ServiceAgents.AuthServer.Dto
{
    public class AuthUserAutenticateDTO
    {
        public TokenAuthViewModel TokenAuth { get; set; }
        public string Image { get; set; }
        public bool PessoaExists { get; set; }
    }
}
