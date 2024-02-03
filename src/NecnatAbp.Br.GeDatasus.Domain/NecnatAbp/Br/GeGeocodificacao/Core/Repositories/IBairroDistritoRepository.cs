using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial interface IBairroDistritoRepository : IRepository<BairroDistrito, Guid>
    {
        Task<BairroDistrito?> GetByCodigoIbgeAsync(string codigoIbge);
        Task<BairroDistrito?> GetByCidadeMunicipioIdAndNomeAsync(Guid cidadeMunicipioId, string nome);
        Task<List<BairroDistrito>> SearchByCidadeMunicipioIdAndNomeContainsAsync(Guid cidadeMunicipioId, string nomeContains);
        Task<int> UpdateAllAtivoAsync(bool ativo);
        Task<BairroDistrito> MaintainAsync(BairroDistrito e, bool autoSave = false);
    }
}
