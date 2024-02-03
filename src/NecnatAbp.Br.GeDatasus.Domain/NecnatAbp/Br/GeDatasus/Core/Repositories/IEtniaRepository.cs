using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NecnatAbp.Br.GePessoaFisica
{
    public partial interface IEtniaRepository : IRepository<Etnia, Guid>
    {
        Task<Etnia?> GetByCodigoCadSusAsync(string codigoCadSus);
    }
}