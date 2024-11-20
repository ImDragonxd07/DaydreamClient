using MelonLoader;
using Daydream.Client.Utility;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRC;
using VRCWSLibary;
using System.Net;
using Newtonsoft.Json;

namespace Daydream.Client.Communication
{
    public class Msg
    {
        public string UserID;
        public string Message;
    }
    internal class DaydreamNetwork
    {

        private static VRCWSLibary.Client client;
        public static System.Collections.Generic.List<string> daydreamadmins = new System.Collections.Generic.List<string>();

        public static void load()
        {
            MelonCoroutines.Start(Start());
            WebClient client = new WebClient();
            string reply = client.DownloadString("https://raw.githubusercontent.com/GITDragonxd07/UnityClientDll/main/admins.txt").Replace("\"", "");
            Utility.Logger.log("Admins: " + reply);
            string[] subs = reply.Split(',');

            foreach (string sub in subs)
            {
                daydreamadmins.Add(sub);

            }
        }
        public static void CheckAdmin()
        {
            if (daydreamadmins.Contains(VRCPlayer.field_Internal_Static_VRCPlayer_0._player.field_Private_APIUser_0.id))
            {
                Utility.Logger.networklog("Im an admin");
                Classes.Daydream.isadmin = true;
            }
        }
        private static IEnumerator Start()

        {
            client = VRCWSLibary.Client.GetClient();
            while (!VRCWSLibary.Client.ClientAvailable())
                yield return null;
            client.RegisterEvent("DaydreamData", (msg) =>
            {
                AsyncUtilsVRCWS.ToMain(() =>
                {
                    var data = msg.GetContentAs<Msg>();
                    Utility.Logger.networklog(data.UserID + " Sent data " + data.Message);
                    if(data.Message == "DaydreamJoin")
                    {
                        adddaydreamplayer(data.UserID,false);
                    }else if (data.Message == "DaydreamJoinBot")
                    {
                        adddaydreamplayer(data.UserID,true);
                    }
                });

            }, false,false);
            Utility.Logger.networklog("Connected to Daydream server");
        }

        public static void adddaydreamplayer(string userid, bool bot)
        {
            Utility.Logger.networklog("Added daydream player " + Utility.Extentions.getPlayerFromPlayerlist(userid).field_Private_APIUser_0.displayName);
            Nameplate customPlate;
            if (bot == true)
            {
                customPlate = new(Utility.Extentions.getPlayerFromPlayerlist(userid), "Daydream Bot", Color.white, containerPrefix: "ProPlates");
                customPlate.Color = Color.cyan;
            }
            else
            {
                if (daydreamadmins.Contains(userid))
                {
                    customPlate = new(Utility.Extentions.getPlayerFromPlayerlist(userid), "Daydream (Admin)", Color.white, containerPrefix: "ProPlates");
                    customPlate.Color = Color.green;
                }
                else
                {
                    customPlate = new(Utility.Extentions.getPlayerFromPlayerlist(userid), "Daydream", Color.white, containerPrefix: "ProPlates");
                    customPlate.Color = Color.red;
                }
            }
        }
        public static void Send(string userid, string sentdata) 
        {
            if(Ui.Pages.Settings.AllowNetworking == true)
            {
                string uuid = VRCPlayer.field_Internal_Static_VRCPlayer_0._player.field_Private_APIUser_0.id;
                Utility.Logger.networklog("Attempting to send " + sentdata + " to user " + userid + " from user " + uuid);

                client.Send(new Message()
                {
                    Method = "DaydreamData",
                    Target = userid,
                    Content = JsonConvert.SerializeObject(new Msg() { UserID = uuid, Message = sentdata })
                });
                Utility.Logger.networklog("Sent data: " + sentdata + " to: " + userid);
            }
        }
    }
}
