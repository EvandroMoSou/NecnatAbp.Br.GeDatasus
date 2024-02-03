﻿using NecnatAbp.Br.GeGeocodificacao;
using NecnatAbp.Dtos;
using System;

namespace NecnatAbp.Br.GePessoaFisica
{
    public partial class CtpsDto : ConcurrencyAuditedEntityDto<Guid>
    {
        public Guid? PessoaFisicaId { get; set; }
        public string? Numero { get; set; }
        public string? Serie { get; set; }
        public DateTime? DataEmissao { get; set; }
        public UnidadeFederativa? UnidadeFederativa { get; set; }
    }
}
