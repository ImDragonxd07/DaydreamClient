using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daydream.Client.Ui.Pages
{
    internal class Settings
    {
        private static VRChatUtilityKit.Ui.SubMenu page;
        public static bool AllowNetworking = true;
        public static void create()
        {
            page = Client.Ui.TabUi.createuipage("Settings");
            page.AddButtonGroup(new VRChatUtilityKit.Ui.ButtonGroup("Client Settings", "ClientSettings", new System.Collections.Generic.List<VRChatUtilityKit.Ui.IButtonGroupElement>()
            {
                  new   Client.Ui.Manager().CreateToggle((state) => Hacks.playerinfo.showplayerinfo = state,"Show player info", "playerinfo", "Shows you players info under their nametag",true),
                  new   Client.Ui.Manager().CreateToggle((state) => AllowNetworking = state,"Allow Networking", "networking", "Allows communication with other clients",true),

            }));
            page.ToggleScrollbar(true);
        }
        public static void open(VRChatUtilityKit.Ui.TabButton tb)
        {
            page.gameObject.SetActive(true);
            tb.SubMenu.OpenSubMenu(page.uiPage);
        }
    }
}
