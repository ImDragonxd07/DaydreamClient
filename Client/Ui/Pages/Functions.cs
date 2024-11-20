
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Daydream.Client.Ui.Pages
{
    internal class Functions
    {
        private static VRChatUtilityKit.Ui.SubMenu page;
        public static void create()
        {
            page = Client.Ui.TabUi.createuipage("Functions");
            page.AddButtonGroup(new VRChatUtilityKit.Ui.ButtonGroup("Player", "Player", new System.Collections.Generic.List<VRChatUtilityKit.Ui.IButtonGroupElement>()
            {
                 new Client.Ui.Manager().CreateToggle((state) => Hacks.Flight.Enable(state), "Fight", "Fight", "Enable flight"),
                 new Client.Ui.Manager().CreateToggle((state) => Hacks.Esp.Enable(state), "Esp", "Esp", "Enable Esp"),
                 new VRChatUtilityKit.Ui.SingleButton(() => Client.Utility.Extentions.ChangeToAvatar(VRCPlayer.field_Internal_Static_VRCPlayer_0,"avtr_95d3ab29-9cd3-450b-ae11-34ccc33664f8"), null, "TEST", "TEST", "TEST"),
                 new Client.Ui.Manager().TempToggle((state) => Hacks.Freecam.Freecam.toggle(), "Freecam", "Freecam", "Enable Freecam"),

                 //new Client.Ui.Manager().CreateToggle((state) => Hacks.ServerLagger.enable(state), "Desync", "Desync", "Lags the server when enabled"),
            }));
            page.AddButtonGroup(new VRChatUtilityKit.Ui.ButtonGroup("Item", "Item", new System.Collections.Generic.List<VRChatUtilityKit.Ui.IButtonGroupElement>()
            {
                new VRChatUtilityKit.Ui.SingleButton(() => Hacks.TpItems.update(), null, "TP Items", "ItemTp", "Tp all items to you"),
                 //new Client.Ui.Manager().CreateToggle((state) => Hacks.ServerLagger.enable(state), "Desync", "Desync", "Lags the server when enabled"),
                 new Client.Ui.Manager().CreateToggle((state) => Hacks.Earrape.toggle(state), "Earrape", "Earrape", "Makes your mic loud"),
                new Client.Ui.Manager().CreateToggle((state) => Hacks.ItemSwastica.enable(state), "Swastika", "SwastikaToggle", "Toggle Swastika"),
                new Client.Ui.Manager().CreateToggle((state) => Hacks.ItemStorm.enable(state), "Storm", "StormToggle", "Toggle Storm"),
            }));
            page.AddButtonGroup(new VRChatUtilityKit.Ui.ButtonGroup("Malicious", "malicious", new System.Collections.Generic.List<VRChatUtilityKit.Ui.IButtonGroupElement>()
            {
                 new VRChatUtilityKit.Ui.SingleButton(() => Hacks.IpPortal.Init(), null, "Ip Grab Portal", "ip", "Drops a portal that grabs the player ip"),
                 new VRChatUtilityKit.Ui.SingleButton(() => Hacks.AdvertisePortal.Init(), null, "Spam windows sound", "spamsound", "Spams windows sound to some client users"),

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
