using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace NecnatAbp.Br.GeGeocodificacao.EntityFrameworkCore
{
    public static partial class GeDatasusDbContextModelCreatingExtensions
    {
        public static void ConfigureGeGeocodificacao(
            this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<Pais>(b =>
            {
                b.ToTable(AbpGeGeocodificacaoDbProperties.DbTablePrefix + "Pais",
                    AbpGeGeocodificacaoDbProperties.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.CodigoIso3166Alpha2).HasMaxLength(PaisConsts.MaxCodigoIso3166Alpha2Length);
                b.Property(x => x.CodigoIso3166Alpha3).HasMaxLength(PaisConsts.MaxCodigoIso3166Alpha3Length);
                b.Property(x => x.CodigoIso3166Numeric).HasMaxLength(PaisConsts.MaxCodigoIso3166NumericLength);
                b.Property(x => x.Nome).IsRequired().HasMaxLength(PaisConsts.MaxNomeLength);
                b.Property(x => x.NomeIngles).HasMaxLength(PaisConsts.MaxNomeInglesLength);
                b.Property(x => x.NomeFrances).HasMaxLength(PaisConsts.MaxNomeFrancesLength);
                b.Property(x => x.InAtivo).IsRequired();
                b.Property(x => x.Origem).IsRequired();

                b.HasIndex(x => x.CodigoIso3166Alpha2).IsUnique();
                b.HasIndex(x => x.CodigoIso3166Alpha3).IsUnique();
                b.HasIndex(x => x.CodigoIso3166Numeric).IsUnique();
                b.HasIndex(x => x.Nome).IsUnique();
            });

            builder.Entity<CidadeMunicipio>(b =>
            {
                b.ToTable(AbpGeGeocodificacaoDbProperties.DbTablePrefix + "CidadeMunicipio",
                    AbpGeGeocodificacaoDbProperties.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.UnidadeFederativa).IsRequired();
                b.Property(x => x.Nome).IsRequired().HasMaxLength(CidadeMunicipioConsts.MaxNomeLength);
                b.Property(x => x.CodigoIbge).HasMaxLength(CidadeMunicipioConsts.MaxCodigoIbgeLength);
                b.Property(x => x.InAtivo).IsRequired();
                b.Property(x => x.Origem).IsRequired();

                b.HasIndex(x => x.CodigoIbge).IsUnique();
                b.HasIndex(x => new { x.UnidadeFederativa, x.Nome }).IsUnique();
            });

            builder.Entity<BairroDistrito>(b =>
            {
                b.ToTable(AbpGeGeocodificacaoDbProperties.DbTablePrefix + "BairroDistrito",
                    AbpGeGeocodificacaoDbProperties.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Nome).IsRequired().HasMaxLength(BairroDistritoConsts.MaxNomeLength);
                b.Property(x => x.CodigoIbge).HasMaxLength(BairroDistritoConsts.MaxCodigoIbgeLength);
                b.Property(x => x.InAtivo).IsRequired();
                b.Property(x => x.Origem).IsRequired();

                b.HasOne(o => o.CidadeMunicipio).WithMany().HasForeignKey(x => x.CidadeMunicipioId).IsRequired().OnDelete(DeleteBehavior.Cascade);

                b.HasIndex(x => x.CodigoIbge).IsUnique();
                b.HasIndex(x => new { x.CidadeMunicipioId, x.Nome }).IsUnique();
            });

            builder.Entity<Subdistrito>(b =>
            {
                b.ToTable(AbpGeGeocodificacaoDbProperties.DbTablePrefix + "Subdistrito",
                    AbpGeGeocodificacaoDbProperties.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Nome).IsRequired().HasMaxLength(SubdistritoConsts.MaxNomeLength);
                b.Property(x => x.CodigoIbge).HasMaxLength(SubdistritoConsts.MaxCodigoIbgeLength);
                b.Property(x => x.InAtivo).IsRequired();
                b.Property(x => x.Origem).IsRequired();

                b.HasOne(o => o.BairroDistrito).WithMany().HasForeignKey(x => x.BairroDistritoId).IsRequired().OnDelete(DeleteBehavior.Cascade);

                b.HasIndex(x => x.CodigoIbge).IsUnique();
                b.HasIndex(x => new { x.BairroDistritoId, x.Nome }).IsUnique();
            });

            builder.Entity<TipoLogradouro>(b =>
            {
                b.ToTable(AbpGeGeocodificacaoDbProperties.DbTablePrefix + "TipoLogradouro",
                    AbpGeGeocodificacaoDbProperties.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Nome).IsRequired().HasMaxLength(TipoLogradouroConsts.MaxNomeLength);
                b.Property(x => x.InAtivo).IsRequired();
                b.Property(x => x.Origem).IsRequired();

                b.HasIndex(x => x.Nome).IsUnique();
            });

            builder.Entity<Logradouro>(b =>
            {
                b.ToTable(AbpGeGeocodificacaoDbProperties.DbTablePrefix + "Logradouro",
                    AbpGeGeocodificacaoDbProperties.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Nome).IsRequired().HasMaxLength(LogradouroConsts.MaxNomeLength);
                b.Property(x => x.NomeAbreviado).HasMaxLength(LogradouroConsts.MaxNomeAbreviadoLength);
                b.Property(x => x.Complemento).HasMaxLength(LogradouroConsts.MaxComplementoLength);
                b.Property(x => x.InAtivo).IsRequired();
                b.Property(x => x.Origem).IsRequired();

                b.HasOne(o => o.Pais).WithMany().HasForeignKey(x => x.PaisId).IsRequired().OnDelete(DeleteBehavior.NoAction);
                b.HasOne(o => o.CidadeMunicipio).WithMany().HasForeignKey(x => x.CidadeMunicipioId).IsRequired().OnDelete(DeleteBehavior.NoAction);
                b.HasOne(o => o.BairroDistrito).WithMany().HasForeignKey(x => x.BairroDistritoId).IsRequired().OnDelete(DeleteBehavior.NoAction);

                b.HasIndex(x => x.Cep).IsUnique();
                b.HasIndex(x => x.Nome);
                b.HasIndex(x => x.CidadeMunicipioId);
            });

            builder.Entity<Geolocalizacao>(b =>
            {
                b.ToTable(AbpGeGeocodificacaoDbProperties.DbTablePrefix + "Geolocalizacao",
                    AbpGeGeocodificacaoDbProperties.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Latitude).HasPrecision(10, 8);
                b.Property(x => x.Longitude).HasPrecision(11, 8);
                b.Property(x => x.InAtivo).IsRequired();
                b.Property(x => x.Origem).IsRequired();

                b.HasOne(o => o.Logradouro).WithMany().HasForeignKey(x => x.LogradouroId).IsRequired().OnDelete(DeleteBehavior.Cascade);

                b.HasIndex(x => new { x.LogradouroId, x.Numero }).IsUnique();
            });
        }
    }
}