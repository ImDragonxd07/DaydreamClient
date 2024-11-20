using Daydream.Client.Utility.Serialization;
using ExitGames.Client.Photon;
using MelonLoader;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib;
using UnityEngine;
using VRC;
using VRC.Core;

namespace Daydream.Client.Protections.PhotonClient
{

    internal static class AntiCrash
    {

        // MODIFIED FROM https://github.com/notunixian/odious/blob/main/ReModCE/Components/PhotonAntiCrashComponent.cs
        public static Dictionary<byte, string> PermaBlockedEvents = new Dictionary<byte, string>();
        public static Dictionary<byte, DateTime> LastEvent = new Dictionary<byte, DateTime>();
        public static Dictionary<byte, int> SpamAmount = new Dictionary<byte, int>();
        public static Dictionary<byte, DateTime> BlockedSpam = new Dictionary<byte, DateTime>();
        public static List<PhotonView> list_8;
        public static int PhotonViewInt = 0;
        static int currentplayers;
        private static System.Collections.Generic.List<PhotonView> photonviews;
        public static bool log = false;
        public static bool enabled = false;

        public static void OnPlayerJoined(VRC.Player player)
        {
            currentplayers++;
        }

        public static void OnPlayerLeft(VRC.Player player)
        {
            currentplayers--;
        }
        public static void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            Utility.Logger.log("Get photon views");
            if (buildIndex != -1)
            {
                return;
            }
            photonviews = Resources.FindObjectsOfTypeAll<PhotonView>().ToList();
        }
        private static Il2CppSystem.Collections.Generic.Dictionary<int, Photon.Realtime.Player> _PhotonPlayers =>
     Photon.Pun.PhotonNetwork.prop_Room_0.prop_Dictionary_2_Int32_Player_0;
        private static string getsendertype(int send)
        {
            try
            {
                if (send == -1)
                {
                    return "Server";
                }

                foreach (var photonPlayer in _PhotonPlayers)
                {
                    if (photonPlayer.Key != send) continue;
                    return photonPlayer.Value.field_Public_Player_0.field_Private_APIUser_0.displayName.ToString();
                }

                return "Unknown";
            }
            catch
            {
                return "error";
            }
        }

        private static string PrintByteArray(byte[] bytes)
        {
            var sb = new StringBuilder("new byte[] { ");
            foreach (var b in bytes)
            {
                sb.Append(b + ", ");
            }
            sb.Append("}");
            return sb.ToString();
        }
        public static bool OnEvent(EventData __0)
        {
            // this might not be the greatest idea for performance, but i'm doing it anyway.
            try
            {
                //int Sender = __0.sender;
                //var player = PlayerManager.field_Private_Static_PlayerManager_0.GetPlayer(Sender);
                //string SenderPlayer = player != null ? player.prop_APIUser_0.displayName : "";
                byte code = __0.Code;
                string sender = getsendertype(__0.Sender);
                if (log == true && sender != "Unknown" && sender != "error" && code != 7 && code != 3)
                {
                    Utility.Logger.log("Photon Log event");
                    Utility.Logger.log("--------------------------------------------------");
                    Utility.Logger.log("Event type: " + code);
                    Utility.Logger.log("Sender: " + sender);
                    Utility.Logger.log("Data: " + PrintByteArray(Serialization.ToByteArray(__0.CustomData)));
                    Utility.Logger.log("--------------------------------------------------");

                }
                if (code == 1 || code == 6 || code == 7 || code == 9 || code == 209 || code == 210 && enabled == true)
                {
                    if (BlockedSpam.ContainsKey(code))
                    {
                        if (DateTime.Now.TimeOfDay > BlockedSpam[code].TimeOfDay)
                        {
                            BlockedSpam.Remove(code);
                            if (SpamAmount.ContainsKey(code))
                            {
                                SpamAmount[code] = 0;
                            }
                        }
                        return false;
                    }
                    if (!LastEvent.ContainsKey(code))
                    {
                        LastEvent.Add(code, DateTime.Now);
                    }
                    else
                    {
                        if (!SpamAmount.ContainsKey(code))
                        {
                            SpamAmount.Add(code, 0);
                        }
                        SpamAmount[code]++;
                        if (DateTime.Now.Subtract(LastEvent[code]).TotalSeconds >= 1.0 && !BlockedSpam.ContainsKey(code))
                        {
                            float num = 1f;
                            int num2 = 0;
                            switch (code)
                            {
                                case 6:
                                    num2 = 100;
                                    num = SpamAmount[code] - 100;
                                    break;
                                case 7:
                                    num2 = 20 * photonviews.Count + currentplayers;
                                    num = 1.5f;
                                    break;
                                case 9:
                                    num2 = 10 * photonviews.Count + currentplayers;
                                    num = 5f;
                                    break;
                                case 1:
                                    num2 = 20 * photonviews.Count;
                                    num = 1.2f;
                                    break;
                                case 210:
                                    num2 = 90;
                                    num = 2.5f;
                                    break;
                                case 209:
                                    num2 = 90;
                                    num = 2.5f;
                                    break;
                            }
                            if (SpamAmount[code] > num2)
                            {
                                DateTime value = DateTime.Now.AddSeconds(num);
                                BlockedSpam.Add(code, value);
                                Utility.Logger.log($"[event {code}] prevented spam for {num} seconds");
                                return false;
                            }
                            SpamAmount[code] = 0;
                            LastEvent[code] = DateTime.Now;
                        }
                    }

                }
            }
            catch (Il2CppException ERROR)
            {
                MelonLogger.Error(ERROR.StackTrace);
                return true;
            }

            return true;
        }
    }
}

