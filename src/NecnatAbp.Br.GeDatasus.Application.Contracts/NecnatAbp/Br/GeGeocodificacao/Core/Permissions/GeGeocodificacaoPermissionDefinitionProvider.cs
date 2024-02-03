using NecnatAbp.Br.GeDatasus.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace NecnatAbp.Br.GeGeocodificacao.Permissions;

public class GeGeocodificacaoPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(GeGeocodificacaoPermissions.GroupName, L("Permission:GeGeocodificacao"));

        var pgPaises = myGroup.AddPermission(GeGeocodificacaoPermissions.Paises.Default, L("Permission:Paises:Default"));
        pgPaises.AddChild(GeGeocodificacaoPermissions.Paises.Create, L("Permission:Paises:Create"));
        pgPaises.AddChild(GeGeocodificacaoPermissions.Paises.Update, L("Permission:Paises:Edit"));
        pgPaises.AddChild(GeGeocodificacaoPermissions.Paises.Delete, L("Permission:Paises:Delete"));

        var pgUnidadesFederativas = myGroup.AddPermission(GeGeocodificacaoPermissions.UnidadesFederativas.Default, L("Permission:UnidadesFederativas:Default"));

        var pgCidadesMunicipios = myGroup.AddPermission(GeGeocodificacaoPermissions.CidadesMunicipios.Default, L("Permission:CidadesMunicipios:Default"));
        pgCidadesMunicipios.AddChild(GeGeocodificacaoPermissions.CidadesMunicipios.Create, L("Permission:CidadesMunicipios:Create"));
        pgCidadesMunicipios.AddChild(GeGeocodificacaoPermissions.CidadesMunicipios.Update, L("Permission:CidadesMunicipios:Edit"));
        pgCidadesMunicipios.AddChild(GeGeocodificacaoPermissions.CidadesMunicipios.Delete, L("Permission:CidadesMunicipios:Delete"));

        var pgBairrosDistritos = myGroup.AddPermission(GeGeocodificacaoPermissions.BairrosDistritos.Default, L("Permission:BairrosDistritos:Default"));
        pgBairrosDistritos.AddChild(GeGeocodificacaoPermissions.BairrosDistritos.Create, L("Permission:BairrosDistritos:Create"));
        pgBairrosDistritos.AddChild(GeGeocodificacaoPermissions.BairrosDistritos.Update, L("Permission:BairrosDistritos:Edit"));
        pgBairrosDistritos.AddChild(GeGeocodificacaoPermissions.BairrosDistritos.Delete, L("Permission:BairrosDistritos:Delete"));

        var pgSubdistritos = myGroup.AddPermission(GeGeocodificacaoPermissions.Subdistritos.Default, L("Permission:Subdistritos:Default"));
        pgSubdistritos.AddChild(GeGeocodificacaoPermissions.Subdistritos.Create, L("Permission:Subdistritos:Create"));
        pgSubdistritos.AddChild(GeGeocodificacaoPermissions.Subdistritos.Update, L("Permission:Subdistritos:Edit"));
        pgSubdistritos.AddChild(GeGeocodificacaoPermissions.Subdistritos.Delete, L("Permission:Subdistritos:Delete"));

        var pgTiposLogradouro = myGroup.AddPermission(GeGeocodificacaoPermissions.TiposLogradouro.Default, L("Permission:TiposLogradouro:Default"));
        pgTiposLogradouro.AddChild(GeGeocodificacaoPermissions.TiposLogradouro.Create, L("Permission:TiposLogradouro:Create"));
        pgTiposLogradouro.AddChild(GeGeocodificacaoPermissions.TiposLogradouro.Update, L("Permission:TiposLogradouro:Edit"));
        pgTiposLogradouro.AddChild(GeGeocodificacaoPermissions.TiposLogradouro.Delete, L("Permission:TiposLogradouro:Delete"));

        var pgLogradouros = myGroup.AddPermission(GeGeocodificacaoPermissions.Logradouros.Default, L("Permission:Logradouros:Default"));
        pgLogradouros.AddChild(GeGeocodificacaoPermissions.Logradouros.Create, L("Permission:Logradouros:Create"));
        pgLogradouros.AddChild(GeGeocodificacaoPermissions.Logradouros.Update, L("Permission:Logradouros:Edit"));
        pgLogradouros.AddChild(GeGeocodificacaoPermissions.Logradouros.Delete, L("Permission:Logradouros:Delete"));

        var pgGeolocalizacoes = myGroup.AddPermission(GeGeocodificacaoPermissions.Geolocalizacoes.Default, L("Permission:Geolocalizacoes:Default"));
        pgGeolocalizacoes.AddChild(GeGeocodificacaoPermissions.Geolocalizacoes.Create, L("Permission:Geolocalizacoes:Create"));
        pgGeolocalizacoes.AddChild(GeGeocodificacaoPermissions.Geolocalizacoes.Update, L("Permission:Geolocalizacoes:Edit"));
        pgGeolocalizacoes.AddChild(GeGeocodificacaoPermissions.Geolocalizacoes.Delete, L("Permission:Geolocalizacoes:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<GeDatasusResource>(name);
    }
}
