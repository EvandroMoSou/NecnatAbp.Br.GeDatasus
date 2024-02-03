using NecnatAbp.AppServices;
using System;
using Volo.Abp.Application.Dtos;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial interface ISubdistritoAppService :
        ICrudsAppService<
            SubdistritoDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateSubdistritoDto,
            SubdistritoResultRequestDto> 
    {
    }
}
