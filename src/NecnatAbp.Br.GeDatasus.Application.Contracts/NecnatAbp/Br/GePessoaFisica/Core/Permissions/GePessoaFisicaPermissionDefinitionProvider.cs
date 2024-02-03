using NecnatAbp.Br.GeDatasus.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace NecnatAbp.Br.GePessoaFisica.Permissions;

public partial class GePessoaFisicaPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(GePessoaFisicaPermissions.GroupName, L("Permission:GePessoaFisica"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<GeDatasusResource>(name);
    }
}
