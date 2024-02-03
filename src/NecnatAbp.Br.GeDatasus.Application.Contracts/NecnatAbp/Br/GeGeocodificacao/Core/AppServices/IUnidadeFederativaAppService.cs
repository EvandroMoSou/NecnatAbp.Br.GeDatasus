using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial interface IUnidadeFederativaAppService : IApplicationService
    {
        Task<List<UnidadeFederativaDto>> GetAsync();
    }
}
