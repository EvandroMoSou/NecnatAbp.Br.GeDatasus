using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace NecnatAbp.Br.GeGeocodificacao.EntityFrameworkCore
{
    public partial interface IGeDatasusDbContext : IEfCoreDbContext
    {
        DbSet<Pais> Paises { get; }
        DbSet<CidadeMunicipio> CidadesMunicipios { get; }
        DbSet<BairroDistrito> BairrosDistritos { get; set; }
        DbSet<Subdistrito> Subdistritos { get; set; }
        DbSet<TipoLogradouro> TiposLogradouro { get; set; }
        DbSet<Logradouro> Logradouros { get; set; }
        DbSet<Geolocalizacao> Geolocalizacoes { get; set; }
    }
}

