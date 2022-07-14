using AutoMapper;
using Sesc.Application.ApplicationServices.Queries.Contracts;
using Sesc.Domain.Habilitacao.Repositories.Contracts;
using Sesc.Domain.Habilitacao.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace Sesc.Application.ApplicationServices.Queries
{
    public class EstadoQueryServiceApplication : IEstadoQueryServiceApplication
    {
        private readonly IEstadoRepository _estadoRepository;
        private readonly IMapper _mapper;

        public EstadoQueryServiceApplication(
            IEstadoRepository estadoRepository,
            IMapper mapper
        ) {
            _estadoRepository = estadoRepository;
            _mapper = mapper;
        }

        public IList<EstadoViewModel> GetEstados()
        {
            return _mapper.Map<IList<EstadoViewModel>>(
                _estadoRepository.GetAll().OrderBy(x => x.Descricao)
            );
        }
    }
}
