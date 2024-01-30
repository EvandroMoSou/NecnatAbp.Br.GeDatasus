using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace NecnatAbp.Br.GeDatasus;

[Dependency(ReplaceServices = true)]
public class GeDatasusBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "GeDatasus";
}
