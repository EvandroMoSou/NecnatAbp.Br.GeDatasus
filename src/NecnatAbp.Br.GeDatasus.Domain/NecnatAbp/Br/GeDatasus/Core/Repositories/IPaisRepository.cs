using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial interface IPaisRepository : IRepository<Pais, Guid>
    {
        Task<Pais?> GetByCodigoCadSusAsync(string codigoCadSus);
    }
}
