using NecnatAbp.AppServices;
using System;
using Volo.Abp.Application.Dtos;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial interface ILogradouroAppService :
        ICrudsAppService<
            LogradouroDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateLogradouroDto,
            LogradouroResultRequestDto>
    {
    }
}
