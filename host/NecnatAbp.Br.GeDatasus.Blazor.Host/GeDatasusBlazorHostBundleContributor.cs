using Volo.Abp.Bundling;

namespace NecnatAbp.Br.GeDatasus.Blazor.Host;

public class GeDatasusBlazorHostBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {

    }

    public void AddStyles(BundleContext context)
    {
        context.Add("main.css", true);
    }
}
