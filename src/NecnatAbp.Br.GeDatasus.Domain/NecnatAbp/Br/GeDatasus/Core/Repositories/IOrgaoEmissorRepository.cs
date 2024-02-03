using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NecnatAbp.Br.GePessoaFisica
{
    public partial interface IOrgaoEmissorRepository : IRepository<OrgaoEmissor, Guid>
    {
        Task<OrgaoEmissor?> GetByCodigoCadSusAsync(string codigoCadSus);
    }
}
