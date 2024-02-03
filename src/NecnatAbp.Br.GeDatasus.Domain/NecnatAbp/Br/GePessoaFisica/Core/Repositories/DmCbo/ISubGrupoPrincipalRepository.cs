using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NecnatAbp.Br.GePessoaFisica.DmCbo
{
    public partial interface ISubGrupoPrincipalRepository : IRepository<SubGrupoPrincipal, Guid>
    {
        Task<SubGrupoPrincipal?> GetByCodigoCboAsync(string codigoCbo);
    }
}
