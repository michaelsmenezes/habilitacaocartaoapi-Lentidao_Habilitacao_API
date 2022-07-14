using Sesc.CrossCutting.ServiceAgents.Sharepoint.Entitites;
using System.Threading.Tasks;

namespace Sesc.CrossCutting.ServiceAgents.Sharepoint.Repositories.Contracts
{
    public interface IAuthTokenRepostiory
    {
        Task<Token> GetTokenClientCredentials();
    }
}
