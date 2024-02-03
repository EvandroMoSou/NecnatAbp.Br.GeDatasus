using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NecnatAbp.Br.GePessoaFisica.DmCbo
{
    public partial interface ISubGrupoRepository : IRepository<SubGrupo, Guid>
    {
        Task<SubGrupo?> GetByCodigoCboAsync(string codigoCbo);
    }
}
