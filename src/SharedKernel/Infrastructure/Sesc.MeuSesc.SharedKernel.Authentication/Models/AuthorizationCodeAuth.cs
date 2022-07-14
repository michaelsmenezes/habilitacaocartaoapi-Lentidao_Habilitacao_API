
namespace Sesc.MeuSesc.SharedKernel.Authentication.Models
{
    public class AuthorizationCodeAuth
    {
        public string Code { get; set; }
        public string Callback { get; set; }
    }
}
