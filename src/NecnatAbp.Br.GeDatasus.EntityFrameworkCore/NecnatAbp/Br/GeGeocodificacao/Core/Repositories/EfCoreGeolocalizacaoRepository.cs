using Microsoft.EntityFrameworkCore;
using NecnatAbp.Br.GeDatasus.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial class EfCoreGeolocalizacaoRepository : EfCoreRepository<GeDatasusDbContext, Geolocalizacao, Guid>, IGeolocalizacaoRepository
    {
        public EfCoreGeolocalizacaoRepository(IDbContextProvider<GeDatasusDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Geolocalizacao?> GetByCepAndNumeroWithLogradouroAsync(int cep, int? numero)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Include(x => x.Logradouro).Where(x => x.Logradouro!.Cep == cep && x.Numero == numero).FirstOrDefaultAsync();
        }

        public async Task<Geolocalizacao?> GetByLogradouroIdAndNumeroAsync(Guid logradouroId, int? numero)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(x => x.LogradouroId == logradouroId && x.Numero == numero).FirstOrDefaultAsync();
        }

        public async Task<Geolocalizacao> MaintainAsync(Geolocalizacao e, bool autoSave = false)
        {
            var eDb = await GetByLogradouroIdAndNumeroAsync(e.LogradouroId, e.Numero);
            var isInsert = eDb == null;

            if (isInsert)
                eDb = new Geolocalizacao();
            else if (eDb!.LogradouroId == e.LogradouroId
                && eDb.Numero == e.Numero
                && (e.Latitude != decimal.MinValue && eDb.Latitude == e.Latitude)
                && (e.Longitude != decimal.MinValue && eDb.Longitude == e.Longitude))
                return eDb;

            eDb!.LogradouroId = e.LogradouroId;
            eDb.Numero = e.Numero;
            eDb.Latitude = e.Latitude;
            eDb.Longitude = e.Longitude;
            eDb.InAtivo = e.InAtivo;
            eDb.Origem = e.Origem;

            if (isInsert)
                return await InsertAsync(eDb, autoSave);
            else
                return await UpdateAsync(eDb, autoSave);
        }
    }
}
