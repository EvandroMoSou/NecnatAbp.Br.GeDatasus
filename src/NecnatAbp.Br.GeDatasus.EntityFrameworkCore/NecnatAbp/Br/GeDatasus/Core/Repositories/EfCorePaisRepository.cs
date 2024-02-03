using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial class EfCorePaisRepository
    {
        public async Task<Pais?> GetByCodigoCadSusAsync(string codigoCadSus)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(x => x.CodigoCadSus == codigoCadSus).FirstOrDefaultAsync();
        }
    }
}
