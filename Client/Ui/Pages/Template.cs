using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daydream.Client.Ui.Pages
{
    internal class Template
    {
        private static VRChatUtilityKit.Ui.SubMenu page;
        public static void create()
        {
            page = Client.Ui.TabUi.createuipage("NAME HERE");
            page.AddButtonGroup(new VRChatUtilityKit.Ui.ButtonGroup("", "", new System.Collections.Generic.List<VRChatUtilityKit.Ui.IButtonGroupElement>()
            {
                //BUTTONS HERE
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
