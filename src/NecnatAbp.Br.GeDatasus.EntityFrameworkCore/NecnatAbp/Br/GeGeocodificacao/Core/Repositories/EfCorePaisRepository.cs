using Microsoft.EntityFrameworkCore;
using NecnatAbp.Br.GeDatasus.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial class EfCorePaisRepository : EfCoreRepository<GeDatasusDbContext, Pais, Guid>, IPaisRepository
    {
        public EfCorePaisRepository(IDbContextProvider<GeDatasusDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Pais?> GetByCodigoIso3166NumericAsync(string codigoIso3166Numeric)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(x => x.CodigoIso3166Numeric == codigoIso3166Numeric).FirstOrDefaultAsync();
        }

        public async Task<Pais?> GetByNomeAsync(string nome)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(x => x.Nome == nome).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateAllAtivoAsync(bool ativo)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.ExecuteUpdateAsync(s => s.SetProperty(e => e.InAtivo, ativo));
        }
    }
}
