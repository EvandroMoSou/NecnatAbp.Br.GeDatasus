using NecnatAbp.Br.GeGeocodificacao;
using System.Threading.Tasks;

namespace NecnatAbp.Br.GeDatasus.Addition.GeGeocodificacao
{
    public interface IDatasusPaisRepository : IPaisRepository
    {
        Task<Pais> GetByCodigoCadSusAsync(string codigoCadSus);
    }
}
