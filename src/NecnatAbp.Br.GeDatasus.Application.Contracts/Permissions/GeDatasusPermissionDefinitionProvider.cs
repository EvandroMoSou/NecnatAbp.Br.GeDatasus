using NecnatAbp.Br.GeDatasus.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace NecnatAbp.Br.GeDatasus.Permissions;

public class GeDatasusPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(GeDatasusPermissions.GroupName, L("Permission:GeDatasus"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<GeDatasusResource>(name);
    }
}
