using Microsoft.EntityFrameworkCore;
using NecnatAbp.Br.GeDatasus.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial class EfCoreCidadeMunicipioRepository : EfCoreRepository<GeDatasusDbContext, CidadeMunicipio, Guid>, ICidadeMunicipioRepository
    {
        public EfCoreCidadeMunicipioRepository(IDbContextProvider<GeDatasusDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<CidadeMunicipio?> GetByCodigoIbgeAsync(string codigoIbge)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(x => x.CodigoIbge == codigoIbge).FirstOrDefaultAsync();
        }

        public async Task<CidadeMunicipio?> GetByUnidadeFederativaAndNomeAsync(UnidadeFederativa unidadeFederativa, string nome)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(x => x.UnidadeFederativa == unidadeFederativa && x.Nome == nome).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateAllAtivoAsync(bool ativo)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.ExecuteUpdateAsync(s => s.SetProperty(e => e.InAtivo, ativo));
        }

        public async Task<CidadeMunicipio> MaintainAsync(CidadeMunicipio e, bool autoSave = false)
        {
            var eDb = await GetByUnidadeFederativaAndNomeAsync(e.UnidadeFederativa, e.Nome);
            var isInsert = eDb == null;

            if (isInsert)
                eDb = new CidadeMunicipio();
            else if (eDb!.UnidadeFederativa == e.UnidadeFederativa
                && eDb.Nome == e.Nome
                && (e.CodigoIbge != null && eDb.CodigoIbge == e.CodigoIbge)
                && eDb.InAtivo == e.InAtivo)
                return eDb;

            eDb!.UnidadeFederativa = e.UnidadeFederativa;
            eDb.Nome = e.Nome;
            eDb.CodigoIbge = e.CodigoIbge;
            eDb.InAtivo = e.InAtivo;
            eDb.Origem = e.Origem;

            if (isInsert)
                return await InsertAsync(eDb, autoSave);
            else
                return await UpdateAsync(eDb, autoSave);
        }
    }
}
