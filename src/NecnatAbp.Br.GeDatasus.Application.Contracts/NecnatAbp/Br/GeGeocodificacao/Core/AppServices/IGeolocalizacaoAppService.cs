using NecnatAbp.AppServices;
using System;
using Volo.Abp.Application.Dtos;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial interface IGeolocalizacaoAppService :
        ICrudsAppService<
            GeolocalizacaoDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateGeolocalizacaoDto,
            GeolocalizacaoResultRequestDto>
    {
    }
}
