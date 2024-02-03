using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NecnatAbp.Br.GePessoaFisica.DmCbo
{
    public partial interface ISinonimoRepository : IRepository<Sinonimo, Guid>
    {
        Task<Sinonimo?> GetByCodigoCboAsync(string codigoCbo);
        Task<List<Sinonimo>> SearchByFamiliaIdOptionalAndTituloContainsWithOcupacaoAAsync(Guid? familiaId, string tituloContains);
    }
}
