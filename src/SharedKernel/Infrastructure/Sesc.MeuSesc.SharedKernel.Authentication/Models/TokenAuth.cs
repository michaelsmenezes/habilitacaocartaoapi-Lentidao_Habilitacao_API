using Sesc.MeuSesc.SharedKernel.Authentication.Models.Bases;
using System.Runtime.Serialization;

namespace Sesc.MeuSesc.SharedKernel.Authentication.Models
{
    [DataContract]
    public class TokenAuth : ModelAuthBase
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "expires_in")]
        public long ExpiresIn { get; set; }

        [DataMember(Name = "token_type")]
        public string TokenType { get; set; }

        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }
    }
}
