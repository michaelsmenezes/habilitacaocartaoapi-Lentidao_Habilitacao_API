using Sesc.MeuSesc.SharedKernel.Authentication.Models;
using Sesc.SharedKernel.Authentication.Services.Base.Contracts;
using System.Threading.Tasks;

namespace Sesc.CrossCutting.ServiceAgents.AuthServer.Services.Contracts
{
    public interface IUserAuthService : IBaseUserAuthService
    {
        Task<UserAuth> AddCleanComerciario(string cpf, string matricula);
        Task<UserAuth> RemoveCleanComerciario(string cpf, string matricula);
        Task<UserAuth> GetUserByCpf(string cpf);
        Task<UserAuth> GetUserByEmail(string email);
        Task<UserAuth> UpdateUser(string email, UserAuth user);
        Task<bool> RequestResetByEmail(string email);
    }
}
