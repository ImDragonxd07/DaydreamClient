using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRChatUtilityKit.Ui;

namespace Daydream.Client.Ui.Pages
{
    internal class Bots
    {
        private static VRChatUtilityKit.Ui.SubMenu page;
        public static void create()
        {
            page = Client.Ui.TabUi.createuipage("Bots");
            page.AddButtonGroup(new VRChatUtilityKit.Ui.ButtonGroup("Functions", "Functions", new System.Collections.Generic.List<VRChatUtilityKit.Ui.IButtonGroupElement>()
            {
                 new VRChatUtilityKit.Ui.SingleButton(() => Client.Ui.Pages.AppBotFunctions.open(Client.Ui.TabUi.myTabButton), null, "Bot Functions", "BotFunctions", "Open bot Functions"),
                  new Client.Ui.Manager().TempToggle((state) => Client.Bots.AppBot.showui = state,"Show gui", "showui", "Starts bot with ui"),
            })); ;
            ButtonGroup bots = new VRChatUtilityKit.Ui.ButtonGroup("Bots", "Bots", new System.Collections.Generic.List<VRChatUtilityKit.Ui.IButtonGroupElement>());
            using (Dictionary<string, int>.ValueCollection.Enumerator enumerator = Client.Bots.AppBot.LoginAndProfile.Values.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    bots.AddButton(new VRChatUtilityKit.Ui.SingleButton(() => Client.Bots.AppBot.CreateBot(enumerator.Current), null, "Start bot " + enumerator.Current, "Startbot" + enumerator.Current, "Creates a application bot"));
                }
            }
            page.AddButtonGroup(bots);
            page.ToggleScrollbar(true);
        }

        public static void open(VRChatUtilityKit.Ui.TabButton tb)
        {
            page.gameObject.SetActive(true);

            tb.SubMenu.OpenSubMenu(page.uiPage);
        }
    }
}
