using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daydream.Client.Ui.Pages
{
    internal class Udon
    {
        private static VRChatUtilityKit.Ui.SubMenu page;
        private static VRChatUtilityKit.Ui.ButtonGroup group;

        public static void create()
        {
            page = Client.Ui.TabUi.createuipage("Udon");
            page.AddButtonGroup(new VRChatUtilityKit.Ui.ButtonGroup("General", "General", new System.Collections.Generic.List<VRChatUtilityKit.Ui.IButtonGroupElement>()
            {
                                 new VRChatUtilityKit.Ui.SingleButton(() => MelonCoroutines.Start(Classes.UdonManager.triggerall()), null, "trigger all", "all", "trigger all events"),
            }));
            page.ToggleScrollbar(true);
            group = new VRChatUtilityKit.Ui.ButtonGroup("Events", "Events", new System.Collections.Generic.List<VRChatUtilityKit.Ui.IButtonGroupElement>());
        }
        public static void open(VRChatUtilityKit.Ui.TabButton tb)
        {
            page.gameObject.SetActive(true);
            tb.SubMenu.OpenSubMenu(page.uiPage);
            group.ClearButtons();
            foreach (var e in Classes.UdonManager.GetUdonBehaviours())
            {
                foreach (var objet in Classes.UdonManager.GetEventNames(e))
                {
                    group.AddButton(new VRChatUtilityKit.Ui.SingleButton(() =>Classes.UdonManager.triggerevent(objet), null, objet, objet, $"trigger {objet}"));
                }
            }

            page.AddButtonGroup(group);
        }
    }
}
