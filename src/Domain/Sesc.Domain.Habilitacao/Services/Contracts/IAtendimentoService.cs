using Sesc.Domain.Habilitacao.Dto;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.MeuSesc.SharedKernel.Application.Services;
using System.Threading.Tasks;

namespace Sesc.Domain.Habilitacao.Services.Contracts
{
    public interface IAtendimentoService : IService<Atendimento>
    {
        Task<Atendimento> Salvar(Atendimento atendimento);
        Task<Atendimento> GetById(int id, bool noTrack = false);
    }
}
