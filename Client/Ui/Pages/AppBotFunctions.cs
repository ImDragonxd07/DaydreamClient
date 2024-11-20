using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements.Controls;

namespace Daydream.Client.Ui.Pages
{
    internal class AppBotFunctions
    {
        private static VRChatUtilityKit.Ui.SubMenu page;
        public static void create()
        {
            page = Client.Ui.TabUi.createuipage("Bot Functions");
            page.AddButtonGroup(new VRChatUtilityKit.Ui.ButtonGroup("Laggers", "Laggers", new System.Collections.Generic.List<VRChatUtilityKit.Ui.IButtonGroupElement>()
            {
               new Client.Ui.Manager().TempToggle((state) => Client.Bots.AppBot.sendcommand("event9", state), "Bot Event9 Lagger", "BotEvent9", "Enable Event9 Lagger for all bots"),
                new Client.Ui.Manager().TempToggle((state) => Client.Bots.AppBot.sendcommand("event6", state), "Bot Event6 Lagger", "BotEvent6", "Enable Event6 Lagger for all bots"),
                new Client.Ui.Manager().TempToggle((state) => Client.Bots.AppBot.sendcommand("event1", state), "Bot Uspeak Lagger", "BotUspeak", "Enable Uspeak Lagger for all bots"),
                                    new Client.Ui.Manager().TempToggle((state) => Client.Bots.AppBot.sendcommand("Quest", state), "Quest Lagger", "Quest", "Enable Quest Lagger"),
            }));

            page.AddButtonGroup(new VRChatUtilityKit.Ui.ButtonGroup("Control", "Control", new System.Collections.Generic.List<VRChatUtilityKit.Ui.IButtonGroupElement>()
            {
                     new Client.Ui.Manager().TempToggle((state) => Client.Bots.AppBot.copyik(Client.Utility.Extentions.GetUserID(VRChatUtilityKit.Utilities.Extensions.ToIUser(VRCPlayer.field_Internal_Static_VRCPlayer_0._player.field_Private_APIUser_0)),state), "Copy Movements","CopyIK", "Makes bots copy you"),
                    new Client.Ui.Manager().TempToggle((state) => Client.Bots.AppBot.orbitme(Client.Utility.Extentions.GetUserID(VRChatUtilityKit.Utilities.Extensions.ToIUser(VRCPlayer.field_Internal_Static_VRCPlayer_0._player.field_Private_APIUser_0)), state), "Orbit", "Orbit", "Makes bot orbit you"),
                    new Client.Ui.Manager().TempToggle((state) => Client.Bots.AppBot.sendcommand("mictoggle", state), "Mic Toggle", "Mic", "Enables bot mics"),

                    new VRChatUtilityKit.Ui.SingleButton(() => Client.Bots.AppBot.botcloneavatar(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_ApiAvatar_0.id), null, "Clone Avatar", "CloneAvatar", "All bots clone your current avatar"),
                    new VRChatUtilityKit.Ui.SingleButton(() => Client.Bots.AppBot.joinme(), null, "Join Me", "JoinMe", "Makes all bots join you"),

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
