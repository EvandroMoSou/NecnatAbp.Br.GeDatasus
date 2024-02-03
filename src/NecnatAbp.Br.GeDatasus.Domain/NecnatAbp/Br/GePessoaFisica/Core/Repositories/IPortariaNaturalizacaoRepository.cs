using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NecnatAbp.Br.GePessoaFisica
{
    public partial interface IPortariaNaturalizacaoRepository : IRepository<PortariaNaturalizacao, Guid>
    {
        Task<PortariaNaturalizacao?> GetByNomeIncompletoAsync(string nomeIncompleto);
    }
}
