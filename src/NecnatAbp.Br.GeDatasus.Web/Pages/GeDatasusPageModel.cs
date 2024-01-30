using NecnatAbp.Br.GeDatasus.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace NecnatAbp.Br.GeDatasus.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class GeDatasusPageModel : AbpPageModel
{
    protected GeDatasusPageModel()
    {
        LocalizationResourceType = typeof(GeDatasusResource);
        ObjectMapperContext = typeof(GeDatasusWebModule);
    }
}
