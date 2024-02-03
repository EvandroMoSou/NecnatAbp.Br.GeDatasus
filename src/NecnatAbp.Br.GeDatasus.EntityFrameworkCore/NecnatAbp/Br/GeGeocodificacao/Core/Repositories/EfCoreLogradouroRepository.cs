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
    public class EfCoreLogradouroRepository : EfCoreRepository<GeDatasusDbContext, Logradouro, Guid>, ILogradouroRepository
    {
        public EfCoreLogradouroRepository(IDbContextProvider<GeDatasusDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Logradouro?> GetByCepAsync(int cep)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(x => x.Cep == cep).FirstOrDefaultAsync();
        }

        public async Task<List<Logradouro>> SearchByCidadeMunicipioIdAndNomeContainsAsync(Guid cidadeMunicipioId, string nomeContains)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(x => x.CidadeMunicipioId == cidadeMunicipioId && x.Nome.Contains(nomeContains)).ToListAsync();
        }
    }
}
