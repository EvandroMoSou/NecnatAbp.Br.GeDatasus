using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace NecnatAbp.Br.GeDatasus.Pages;

public class IndexModel : GeDatasusPageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
