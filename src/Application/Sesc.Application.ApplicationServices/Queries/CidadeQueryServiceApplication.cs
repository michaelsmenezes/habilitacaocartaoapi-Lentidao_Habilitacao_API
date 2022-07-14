using AutoMapper;
using Sesc.Application.ApplicationServices.Queries.Contracts;
using Sesc.CrossCutting.Validation.BaseException;
using Sesc.Domain.Habilitacao.Repositories.Contracts;
using Sesc.Domain.Habilitacao.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sesc.Application.ApplicationServices.Queries
{
    public class CidadeQueryServiceApplication : ICidadeQueryServiceApplication
    {
        private readonly ICidadeRepository _cidadeRepository;
        private readonly IMapper _mapper;

        public CidadeQueryServiceApplication(
            ICidadeRepository cidadeRepository,
            IMapper mapper
        ) {
            _cidadeRepository = cidadeRepository;
            _mapper = mapper;
        }

        public IList<CidadeViewModel> GetCidadesByUf(string uf)
        {
            var cidades = _cidadeRepository.FindBy(c => c.Estado.Uf.ToUpper() == uf.ToUpper())
                .OrderBy(c => c.Descricao)
                .ToList();

            return _mapper.Map<IList<CidadeViewModel>>(cidades);
        }

        public IList<CidadeViewModel> GetListaMunicipiosResponsaveis()
        {
            var cidades = _cidadeRepository.GetListaMunicipiosResponsaveis()
                .OrderBy(c => c.Descricao)
                .ToList();

            return _mapper.Map<IList<CidadeViewModel>>(cidades);
        }
    }
}
