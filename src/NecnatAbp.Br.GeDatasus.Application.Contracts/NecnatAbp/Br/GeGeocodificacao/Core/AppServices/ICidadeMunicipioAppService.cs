using NecnatAbp.AppServices;
using System;
using Volo.Abp.Application.Dtos;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial interface ICidadeMunicipioAppService :
        ICrudsAppService<
            CidadeMunicipioDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateCidadeMunicipioDto,
            CidadeMunicipioResultRequestDto> 
    {
    }
}
