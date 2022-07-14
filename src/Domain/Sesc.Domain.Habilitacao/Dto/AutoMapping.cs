using AutoMapper;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.Domain.Habilitacao.ViewModel;

namespace Sesc.Domain.Habilitacao.Dto
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            //ENTITY FOR DTO
            CreateMap<Solicitacao, SolicitacaoDto>().ReverseMap();
            CreateMap<Pessoa, PessoaDto>()
                .Include<Titular, TitularDto>()
                .Include<Responsavel, ResponsavelDto>()
                .Include<Dependente, DependenteDto>();
            CreateMap<Titular, TitularDto>();
            CreateMap<Responsavel, ResponsavelDto>();
            CreateMap<Dependente, DependenteDto>();

            CreateMap<Contato, ContatoDto>().ReverseMap();
            CreateMap<Cidade, CidadeDto>().ReverseMap();
            CreateMap<Estado, EstadoDto>().ReverseMap();
            CreateMap<InformacaoProfissional, InformacaoProfissionalDto>().ReverseMap();
            CreateMap<Documento, DocumentoDto>().ReverseMap();
            CreateMap<Atendimento, AtendimentoDto>().ReverseMap();

            //ENTITY FOR VIEW MODEL
            CreateMap<Solicitacao, SolicitacaoViewModel>().ReverseMap();
            CreateMap<Pessoa, PessoaViewModel>()
                .Include<Titular, TitularViewModel>()
                .Include<Responsavel, ResponsavelViewModel>()
                .Include<Dependente, DependenteViewModel>();
            CreateMap<Titular, TitularViewModel>();
            CreateMap<Responsavel, ResponsavelViewModel>();
            CreateMap<Dependente, DependenteViewModel>();
            CreateMap<Documento, DocumentoViewModel>();

            CreateMap<Contato, ContatoViewModel>().ReverseMap();
            CreateMap<Cidade, CidadeViewModel>()
                //.ForMember( x => x.CidadeResponsavelDescricao, p => p.MapFrom( a => a.CidadeResponsavel != null ? a.CidadeResponsavel.Descricao : ""))
                .ReverseMap();

            CreateMap<Estado, EstadoViewModel>().ReverseMap();
            CreateMap<InformacaoProfissional, InformacaoProfissionalViewModel>().ReverseMap();
            CreateMap<Documento, DocumentoViewModel>().ReverseMap();
            CreateMap<Atendimento, AtendimentoViewModel>().ReverseMap();

            //DTO FOR VIEW MODEL
            CreateMap<SolicitacaoDto, SolicitacaoViewModel>().ReverseMap();
            CreateMap<SolicitacaoFiltrosDto, SolicitacaoFiltrosViewModel>().ReverseMap();
            CreateMap<TitularDto, TitularViewModel>().ReverseMap();
            CreateMap<ResponsavelDto, ResponsavelViewModel>().ReverseMap();
            CreateMap<DependenteDto, DependenteViewModel>().ReverseMap();
            CreateMap<ContatoDto, ContatoViewModel>().ReverseMap();
            CreateMap<CidadeDto, CidadeViewModel>().ReverseMap();
            CreateMap<EstadoDto, EstadoViewModel>().ReverseMap();
            CreateMap<InformacaoProfissionalDto, InformacaoProfissionalViewModel>().ReverseMap();
            CreateMap<DocumentoDto, DocumentoViewModel>().ReverseMap();
            CreateMap<AtendimentoDto, AtendimentoViewModel>().ReverseMap();

            // VIEWMODEL FOR DTO
            CreateMap<SolicitacaoViewModel, Solicitacao>().ReverseMap();
            CreateMap<SolicitacaoFiltrosViewModel, SolicitacaoFiltrosDto>().ReverseMap();
            CreateMap<TitularViewModel, Titular>().ReverseMap();
            CreateMap<ResponsavelViewModel, Responsavel>().ReverseMap();
            CreateMap<DependenteViewModel, Dependente>().ReverseMap();
            CreateMap<ContatoViewModel, Contato>().ReverseMap();
            CreateMap<CidadeViewModel, Cidade>().ReverseMap();
            CreateMap<EstadoViewModel, Estado>().ReverseMap();
            CreateMap<InformacaoProfissionalViewModel, InformacaoProfissionalDto>().ReverseMap();
            CreateMap<DocumentoViewModel, DocumentoDto>().ReverseMap();
            CreateMap<AtendimentoViewModel, AtendimentoDto>().ReverseMap();

            //VIEWM MODEL FOR ENTITY
            CreateMap<InformacaoProfissionalViewModel, InformacaoProfissional>().ReverseMap();

            // DTO FOR ENTITY
            CreateMap<SolicitacaoDto, Solicitacao>().ReverseMap();
            CreateMap<DependenteDto, Dependente>().ReverseMap();
            CreateMap<TitularDto, Titular>().ReverseMap();
            CreateMap<ContatoDto, Contato>().ReverseMap();
            CreateMap<CidadeDto, Cidade>().ReverseMap();
            CreateMap<EstadoDto, Estado>().ReverseMap();
            CreateMap<InformacaoProfissionalDto, InformacaoProfissional>().ReverseMap();
            CreateMap<DocumentoDto, Documento>().ReverseMap();
        }
    }
}
