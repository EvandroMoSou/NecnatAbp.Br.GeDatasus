using NecnatAbp.AppServices;
using System;
using Volo.Abp.Application.Dtos;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial interface ITipoLogradouroAppService :
        ICrudsAppService<
            TipoLogradouroDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateTipoLogradouroDto,
            TipoLogradouroResultRequestDto>
    {

    }
}
