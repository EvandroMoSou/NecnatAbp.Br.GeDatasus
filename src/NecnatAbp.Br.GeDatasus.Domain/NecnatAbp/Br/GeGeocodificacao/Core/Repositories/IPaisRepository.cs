using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial interface IPaisRepository : IRepository<Pais, Guid>
    {
        Task<Pais?> GetByCodigoIso3166NumericAsync(string codigoIso3166Numeric);
        Task<Pais?> GetByNomeAsync(string nome);
        Task<int> UpdateAllAtivoAsync(bool ativo);
    }
}
