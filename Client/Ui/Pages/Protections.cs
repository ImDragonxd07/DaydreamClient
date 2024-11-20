using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Daydream.Client.Ui.Pages
{
    internal class Protections
    {
        private static VRChatUtilityKit.Ui.SubMenu page;
        public static void create()
        {
            page = Client.Ui.TabUi.createuipage("Protections");
            page.AddButtonGroup(new VRChatUtilityKit.Ui.ButtonGroup("Protections", "Protections", new System.Collections.Generic.List<VRChatUtilityKit.Ui.IButtonGroupElement>()
            {
                  new Client.Ui.Manager().CreateToggle((state) => Client.Protections.PhotonClient.AntiCrash.log = state,"Log Photon", "LogPhoton", "Logs Photon events"),
                  new Client.Ui.Manager().CreateToggle((state) => Hacks.RpcLog.Log = state,"Rpc Log", "rpclog", "Logs Rpc events"),
                  new Client.Ui.Manager().CreateToggle((state) => Client.Protections.PhotonClient.AntiCrash.enabled = state,"Photon AntiCrash", "PhotonAntiCrash", "Limits the number of photon events your client processes",true),

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
