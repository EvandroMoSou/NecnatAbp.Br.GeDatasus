using System;
using Volo.Abp.Domain.Repositories;

namespace NecnatAbp.Br.GePessoaFisica
{
    public partial interface IOrgaoEmissorRepository : IRepository<OrgaoEmissor, Guid>
    {
    }
}
