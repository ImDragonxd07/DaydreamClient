using Il2CppSystem.Net;
using Il2CppSystem.Reflection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using VRC;
using VRC.Core;
using VRC.UI;

namespace Daydream.Client.Hacks
{
    internal class RipAvatar
    {
        // Token: 0x0600022C RID: 556 RVA: 0x000125C4 File Offset: 0x000107C4
        private static ApiAvatar avatar;
        public static void DownloadSelect()
        {
            if (Ui.TabUi.getselectedplayer() != null)
            {
                bool flag = !Directory.Exists(Path.Combine(Environment.CurrentDirectory, "Daydream Downloads"));
                if (flag)
                {
                    Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "Daydream Downloads"));
                }
                //download(, Path.Combine(Environment.CurrentDirectory, "VRCA") + "/" + VRCUtils.ActiveUserInUserInfoMenu.displayName);
                System.Net.WebClient webClient = new System.Net.WebClient();
                Utility.Logger.log(Ui.TabUi.getselectedplayer().displayName.ToString());
                foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Player"))
                {
                    
                    if (gameObject.GetComponent<VRCPlayer>().prop_VRCPlayerApi_0.displayName.ToString() == Ui.TabUi.getselectedplayer().displayName.ToString())
                    {
                        Utility.Logger.log("Found Player " + gameObject.transform.name);
                        Utility.Logger.log("Download " + gameObject.GetComponent<VRCPlayer>().field_Private_ApiAvatar_0.assetUrl);
                        Downloadavatar(gameObject.GetComponent<VRCPlayer>().field_Private_ApiAvatar_0.assetUrl, Path.Combine(Path.Combine(Environment.CurrentDirectory, "Daydream Downloads"), Ui.TabUi.getselectedplayer().displayName.ToString() + "'s_Avatar.vrca"));
                    }
                }

            }
            // Token: 0x04000192 RID: 402
        }
        internal static void Downloadavatar(string url, string filename)
        {
            System.Net.WebClient webClient = new System.Net.WebClient();
            webClient.Headers.Add("user-agent", "VRCA");
            webClient.DownloadFileAsync(new Uri(url), filename);
            Ui.Ui.Notification("Downloaded Avatar");
        }
    }
}
