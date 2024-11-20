using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Daydream.Client.Ui.Pages
{
    internal class ReMod
    {
        private static VRChatUtilityKit.Ui.SubMenu page;
        public static void create()
        {
            page = Client.Ui.TabUi.createuipage("ReMod");
            page.AddButtonGroup(new VRChatUtilityKit.Ui.ButtonGroup("Account", "Account", new System.Collections.Generic.List<VRChatUtilityKit.Ui.IButtonGroupElement>()
            {
                new VRChatUtilityKit.Ui.Label(Client.Utility.SaveData.getpin(),"pin","pin"),
                new VRChatUtilityKit.Ui.SingleButton(() => Client.Avatar.AvatarSearch.loginpopup(), null, "Login", "Login", "Login to remod"),
                new VRChatUtilityKit.Ui.SingleButton(() => Client.Utility.SaveData.setpin("none"), null, "Forget Pin", "forget", "Forgets set remod pin"),
                new VRChatUtilityKit.Ui.SingleButton(() => Application.OpenURL("https://remod-ce.requi.dev/api/pin.php"), null, "Reset Pin", "reset", "Resets remod pin"),
                new VRChatUtilityKit.Ui.SingleButton(() => Application.OpenURL($"{Classes.Daydream.BaseWebsiteurl}/remod.html"), null, "Help", "help", "Opens the Daydream help website"),

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
