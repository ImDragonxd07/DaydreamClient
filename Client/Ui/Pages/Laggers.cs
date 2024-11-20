using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VRChatUtilityKit.Ui;

namespace Daydream.Client.Ui.Pages
{
    internal class Laggers
    {
        public static bool acceptedwarning = false;
        private static VRChatUtilityKit.Ui.SubMenu page;
        public static void create()
        {
            page = Client.Ui.TabUi.createuipage("Laggers");
            page.AddButtonGroup(new VRChatUtilityKit.Ui.ButtonGroup("Laggers", "Laggers", new System.Collections.Generic.List<VRChatUtilityKit.Ui.IButtonGroupElement>()
            {
                //CANT BE SEARLIALISED
                   new Client.Ui.Manager().TempToggle((state) => Hacks.ItemLagger.enable(state),"Item Lagger", "ItemLaggerToggle", "Lags world with Items"),
                  new Client.Ui.Manager().TempToggle((state) => Hacks.event9.enable(state), "Event9 Lagger", "Event9", "Enable Event9 Lagger"),
                   new Client.Ui.Manager().TempToggle((state) => Hacks.event6.enable(state), "Event6 Lagger", "Event6", "Enable Event6 Lagger"),
                   new Client.Ui.Manager().TempToggle((state) => Hacks.event1.enable(state), "Uspeak Lagger", "Uspeak", "Enable Uspeak Lagger"),
                    new Client.Ui.Manager().TempToggle((state) => Hacks.Quest_Lag.enable(state), "Quest Lagger", "Quest", "Enable Quest Lagger"),

            }));
            page.AddButtonGroup(new VRChatUtilityKit.Ui.ButtonGroup("Crashers", "Crashers", new System.Collections.Generic.List<VRChatUtilityKit.Ui.IButtonGroupElement>()
            {
                //CANT BE SEARLIALISED
                       // new VRChatUtilityKit.Ui.SingleButton(() => Hacks.RamCrash.ramceash(), null, "Ram Crash", "ram", "Ram crash"), // ban

            }));
            page.ToggleScrollbar(true);
        }
        public static void open(VRChatUtilityKit.Ui.TabButton tb)
        {
            page.gameObject.SetActive(true);

            if (acceptedwarning == false)
            {
                UiManager.OpenSmallPopup("Warning", "Spamming events can get you BANNED (Continue at your own risk)", "I understand", new Action(UiManager.ClosePopup));
            }
            acceptedwarning = true;
            tb.SubMenu.OpenSubMenu(page.uiPage);

        }
    }
}
