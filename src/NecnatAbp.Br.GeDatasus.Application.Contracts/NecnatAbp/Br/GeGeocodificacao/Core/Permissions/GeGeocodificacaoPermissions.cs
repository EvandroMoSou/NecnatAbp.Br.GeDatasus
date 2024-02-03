using Volo.Abp.Reflection;

namespace NecnatAbp.Br.GeGeocodificacao.Permissions;

public class GeGeocodificacaoPermissions
{
    public const string GroupName = "GeGeocodificacao";

    public static class Paises
    {
        public const string Default = GroupName + ".Paises";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static class UnidadesFederativas
    {
        public const string Default = GroupName + ".UnidadesFederativas";
    }

    public static class CidadesMunicipios
    {
        public const string Default = GroupName + ".CidadesMunicipios";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static class BairrosDistritos
    {
        public const string Default = GroupName + ".BairrosDistritos";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static class Subdistritos
    {
        public const string Default = GroupName + ".Subdistritos";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static class TiposLogradouro
    {
        public const string Default = GroupName + ".TiposLogradouro";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static class Logradouros
    {
        public const string Default = GroupName + ".Logradouros";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static class Geolocalizacoes
    {
        public const string Default = GroupName + ".Geolocalizacoes";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(GeGeocodificacaoPermissions));
    }
}
