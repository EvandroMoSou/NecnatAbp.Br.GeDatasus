using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial interface ITipoLogradouroRepository : IRepository<TipoLogradouro, Guid>
    {
        Task<TipoLogradouro?> GetByCodigoCadSusAsync(string codigoCadSus);
    }
}
