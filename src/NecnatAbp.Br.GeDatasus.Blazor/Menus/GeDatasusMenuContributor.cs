using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace NecnatAbp.Br.GeDatasus.Blazor.Menus;

public class GeDatasusMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        context.Menu.AddItem(new ApplicationMenuItem(GeDatasusMenus.Prefix, displayName: "GeDatasus", "/GeDatasus", icon: "fa fa-globe"));

        return Task.CompletedTask;
    }
}
