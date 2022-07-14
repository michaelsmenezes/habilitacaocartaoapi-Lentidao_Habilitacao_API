using Sesc.Domain.Habilitacao.Dto;
using System.Threading.Tasks;
using Sesc.Domain.Habilitacao.Repositories.Contracts;
using Sesc.Domain.Habilitacao.Entities;
using AutoMapper;
using Sesc.MeuSesc.SharedKernel.Application.Services;
using Sesc.MeuSesc.SharedKernel.Infrastructure.UnitOfWork;
using Sesc.Domain.Habilitacao.Validator;
using Sesc.Domain.Habilitacao.Services.Contracts;
using System.Linq;

namespace Sesc.Domain.Habilitacao.Services
{
    public class DependenteService : Service<Dependente, IDependenteRepository>, IDependenteService
    {
        private readonly IDocumentoRepository _documentoRepository;

        public DependenteService(
            IUnitOfWork unitOfWork,
            IDependenteRepository repository,
            IDocumentoRepository documentoRepository,
            IMapper mapper) : base(unitOfWork, repository, mapper)
        {
            _documentoRepository = documentoRepository;
        }

        public async Task<Dependente> GetById(int id)
        {
            return _repository.FindByInclude(
                d => d.Id == id,
                t => t.Titular
            ).FirstOrDefault();
        }

        public async Task Deletar(Dependente dependente)
        {
            if (dependente != null && 
                dependente.Documentos != null && 
                dependente.Documentos.Count > 0
            ) {
                dependente.Documentos.ToList().ForEach(documento =>
                {
                    _documentoRepository.Delete(documento);
                });
            }

            _repository.Deletar(dependente);
        }

        public async Task Alterar(Dependente dependente)
        {
            _repository.Alterar(dependente);
        }
    }
}
