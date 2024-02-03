using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial interface ISubdistritoRepository : IRepository<Subdistrito, Guid>
    {
        Task<Subdistrito?> GetByCodigoIbgeAsync(string codigoIbge);
        Task<Subdistrito?> GetByCodigoIbgeWithBairroDistritoAsync(string codigoIbge);
        Task<Subdistrito?> GetByBairroDistritoIdAndNomeAsync(Guid bairroDistritoId, string nome);
        Task<List<Subdistrito>> SearchByCidadeMunicipioIdAndNomeContainsWithBairroDistritoAsync(Guid cidadeMunicipioId, string nomeContains);
        Task<int> UpdateAllAtivoAsync(bool ativo);
    }
}
