using AutoMapper;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.Domain.Habilitacao.Repositories.Contracts;
using Sesc.Domain.Habilitacao.Services.Contracts;
using Sesc.MeuSesc.SharedKernel.Application.Services;
using Sesc.MeuSesc.SharedKernel.Infrastructure.UnitOfWork;
using System.Threading.Tasks;

namespace Sesc.Domain.Habilitacao.Services
{
    public class AtendimentoService : Service<Atendimento, IAtendimentoRepository>, IAtendimentoService
    {
        public AtendimentoService(
            IUnitOfWork unitOfWork,
            IAtendimentoRepository repository,
            IMapper mapper) : base(unitOfWork, repository, mapper)
        {
        }


        public async Task<Atendimento> GetById(int id, bool noTrack = false)
        {
            return _repository.GetById(id, noTrack);
        }

        public async Task<Atendimento> Salvar(Atendimento Atendimento)
        {
            _repository.Salvar(Atendimento);

            return _mapper.Map<Atendimento>(Atendimento);
        }

    }
}
