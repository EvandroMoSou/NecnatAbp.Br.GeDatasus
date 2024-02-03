using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NecnatAbp.Br.GePessoaFisica.DmCbo
{
    public partial interface IFamiliaRepository : IRepository<Familia, Guid>
    {
        Task<Familia?> GetByCodigoCboAsync(string codigoCbo);
    }
}
