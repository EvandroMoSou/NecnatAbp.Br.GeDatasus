using NecnatAbp.Br.GeDatasus.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial class EfCoreTipoLogradouroRepository : EfCoreRepository<GeDatasusDbContext, TipoLogradouro, Guid>, ITipoLogradouroRepository
    {
        public EfCoreTipoLogradouroRepository(IDbContextProvider<GeDatasusDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
