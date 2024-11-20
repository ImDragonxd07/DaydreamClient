using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Daydream.Client.Ui.Pages
{
    internal class Utility
    {

        private static VRChatUtilityKit.Ui.SubMenu page;
        public static void create()
        {
            page = Client.Ui.TabUi.createuipage("Utility");
            Hacks.DisableChairs.load();
            page.AddButtonGroup(new VRChatUtilityKit.Ui.ButtonGroup("Helpful", "Helpful", new System.Collections.Generic.List<VRChatUtilityKit.Ui.IButtonGroupElement>()
            {
                new Client.Ui.Manager().CreateToggle((state) => Hacks.DisableChairs.toggle(state),"Disable chairs","disablechairs","Disables all chairs"),
            }));
            page.AddButtonGroup(new VRChatUtilityKit.Ui.ButtonGroup("Fun", "Fun", new System.Collections.Generic.List<VRChatUtilityKit.Ui.IButtonGroupElement>()
            {
                new Client.Ui.Manager().CreateToggle((state) => Client.Utility.Logger.owo = state,"OwO Logger","OwOLogger","Makes your log owoified (makes log kinda useless)"),
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
