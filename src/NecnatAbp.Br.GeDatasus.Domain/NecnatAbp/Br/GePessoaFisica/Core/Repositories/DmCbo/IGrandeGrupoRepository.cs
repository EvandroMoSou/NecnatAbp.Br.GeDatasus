using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NecnatAbp.Br.GePessoaFisica.DmCbo
{
    public partial interface IGrandeGrupoRepository : IRepository<GrandeGrupo, Guid>
    {
        Task<GrandeGrupo?> GetByCodigoCboAsync(string codigoCbo);
    }
}
