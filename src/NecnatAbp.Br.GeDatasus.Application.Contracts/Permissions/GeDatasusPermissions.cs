using Volo.Abp.Reflection;

namespace NecnatAbp.Br.GeDatasus.Permissions;

public class GeDatasusPermissions
{
    public const string GroupName = "GeDatasus";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(GeDatasusPermissions));
    }
}
