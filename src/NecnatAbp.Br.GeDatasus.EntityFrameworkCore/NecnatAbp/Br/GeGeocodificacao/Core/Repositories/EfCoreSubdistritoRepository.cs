using Microsoft.EntityFrameworkCore;
using NecnatAbp.Br.GeDatasus.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial class EfCoreSubdistritoRepository : EfCoreRepository<GeDatasusDbContext, Subdistrito, Guid>, ISubdistritoRepository
    {
        public EfCoreSubdistritoRepository(IDbContextProvider<GeDatasusDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Subdistrito?> GetByCodigoIbgeAsync(string codigoIbge)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(x => x.CodigoIbge == codigoIbge).FirstOrDefaultAsync();
        }

        public async Task<Subdistrito?> GetByCodigoIbgeWithBairroDistritoAsync(string codigoIbge)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Include(x => x.BairroDistrito).Where(x => x.CodigoIbge == codigoIbge).FirstOrDefaultAsync();
        }

        public async Task<Subdistrito?> GetByBairroDistritoIdAndNomeAsync(Guid bairroDistritoId, string nome)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(x => x.BairroDistritoId == bairroDistritoId && x.Nome == nome).FirstOrDefaultAsync();
        }

        public async Task<List<Subdistrito>> SearchByCidadeMunicipioIdAndNomeContainsWithBairroDistritoAsync(Guid cidadeMunicipioId, string nomeContains)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Include(x => x.BairroDistrito).Where(x => x.BairroDistrito!.CidadeMunicipioId == cidadeMunicipioId && x.Nome.Contains(nomeContains)).ToListAsync();
        }

        public async Task<int> UpdateAllAtivoAsync(bool ativo)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.ExecuteUpdateAsync(s => s.SetProperty(e => e.InAtivo, ativo));
        }
    }
}
