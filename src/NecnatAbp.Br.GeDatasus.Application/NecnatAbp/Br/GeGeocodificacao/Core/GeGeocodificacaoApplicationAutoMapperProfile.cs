using AutoMapper;

namespace NecnatAbp.Br.GeGeocodificacao;

public class GeGeocodificacaoApplicationAutoMapperProfile : Profile
{
    public GeGeocodificacaoApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Pais, PaisDto>();
        CreateMap<CreateUpdatePaisDto, Pais>();
    }
}
