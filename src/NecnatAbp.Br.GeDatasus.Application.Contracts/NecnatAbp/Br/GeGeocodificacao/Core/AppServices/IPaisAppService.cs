using NecnatAbp.AppServices;
using System;
using Volo.Abp.Application.Dtos;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial interface IPaisAppService :
        ICrudsAppService<
            PaisDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdatePaisDto,
            PaisResultRequestDto>
    {
    }
}
