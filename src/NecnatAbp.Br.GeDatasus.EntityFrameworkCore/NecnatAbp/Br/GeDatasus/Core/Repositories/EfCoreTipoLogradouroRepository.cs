using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial class EfCoreTipoLogradouroRepository
    {
        public async Task<TipoLogradouro?> GetByCodigoCadSusAsync(string codigoCadSus)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(x => x.CodigoCadSus == codigoCadSus).FirstOrDefaultAsync();
        }
    }
}
