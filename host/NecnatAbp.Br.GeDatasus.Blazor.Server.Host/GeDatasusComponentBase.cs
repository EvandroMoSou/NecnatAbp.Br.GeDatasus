using NecnatAbp.Br.GeDatasus.Localization;
using Volo.Abp.AspNetCore.Components;

namespace NecnatAbp.Br.GeDatasus.Blazor.Server.Host;

public abstract class GeDatasusComponentBase : AbpComponentBase
{
    protected GeDatasusComponentBase()
    {
        LocalizationResource = typeof(GeDatasusResource);
    }
}
