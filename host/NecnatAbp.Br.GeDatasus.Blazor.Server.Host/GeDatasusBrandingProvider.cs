using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace NecnatAbp.Br.GeDatasus.Blazor.Server.Host;

[Dependency(ReplaceServices = true)]
public class GeDatasusBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "GeDatasus";
}
