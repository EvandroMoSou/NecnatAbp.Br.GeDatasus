using NecnatAbp.AppServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial interface IBairroDistritoAppService :
        ICrudsAppService<
            BairroDistritoDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateBairroDistritoDto,
            BairroDistritoResultRequestDto> 
    {
        Task<List<BairroDistritoDto>> SearchFallbackSubdistritoAsync(BairroDistritoFallbackResultRequestDto input);
    }
}
