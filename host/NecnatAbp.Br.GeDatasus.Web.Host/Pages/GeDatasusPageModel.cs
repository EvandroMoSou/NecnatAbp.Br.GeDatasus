using NecnatAbp.Br.GeDatasus.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace NecnatAbp.Br.GeDatasus.Pages;

public abstract class GeDatasusPageModel : AbpPageModel
{
    protected GeDatasusPageModel()
    {
        LocalizationResourceType = typeof(GeDatasusResource);
    }
}
