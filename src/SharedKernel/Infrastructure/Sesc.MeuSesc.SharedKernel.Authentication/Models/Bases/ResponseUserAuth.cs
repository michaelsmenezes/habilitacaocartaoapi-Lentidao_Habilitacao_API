using System.Runtime.Serialization;

namespace Sesc.MeuSesc.SharedKernel.Authentication.Models.Bases
{
    [DataContract]
    public abstract class ResponseUserAuth : ModelAuthBase
    {
        public int StatusCode { get; set; }
        public bool IsSuccessStatusCode { get; set; }
    }
}
