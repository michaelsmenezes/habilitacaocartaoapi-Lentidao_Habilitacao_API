using Sesc.Domain.Habilitacao.Enum;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sesc.Domain.Habilitacao.Dto
{
    public class SolicitacaoDto : DtoBase
    {
        public int Id { get; set; }
        public string Cpf { get; set; }
        public SolicitacaoTipoEnum Tipo { get; set; }
        public DateTime DataRegistro { get; set; }
        public SolicitacaoSituacaoEnum Situacao { get; set; }
        public SolicitacaoCategoriaEnum Categoria { get; set; }
        public TitularDto Titular { get; set; }
        public ICollection<AtendimentoDto> Atendimentos { get; set; }
    }
}
