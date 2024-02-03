using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial interface ICidadeMunicipioRepository : IRepository<CidadeMunicipio, Guid>
    {
        Task<CidadeMunicipio?> GetByCodigoIbgeAsync(string codigoIbge);
        Task<CidadeMunicipio?> GetByUnidadeFederativaAndNomeAsync(UnidadeFederativa unidadeFederativa, string nome);
        Task<int> UpdateAllAtivoAsync(bool ativo);
        Task<CidadeMunicipio> MaintainAsync(CidadeMunicipio e, bool autoSave = false);
    }
}
