using ExitGames.Client.Photon;
using Harmony;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib;
using UnityEngine;
using VRC.SDKBase;
using static VRC.SDKBase.VRC_EventHandler;

namespace Daydream.Client.Hacks
{
    internal class RpcLog
    {
        private static readonly Dictionary<string, (int, int)> rpcs = new Dictionary<string, (int, int)>()
        {
            { "Generic", (500, 500) },
            // { "ReceiveVoiceStatsSyncRPC", (348, 64) },
            { "InformOfBadConnection", (64, 6) },
            { "initUSpeakSenderRPC", (256, 6) },
            { "InteractWithStationRPC", (128, 32) },
            { "SpawnEmojiRPC", (128, 6) },
            { "SanityCheck", (256, 32) },
            { "PlayEmoteRPC", (256, 6) },
            { "TeleportRPC", (256, 16) },
            { "CancelRPC", (256, 32) },
            { "SetTimerRPC", (256, 64) },
            { "_DestroyObject", (512, 128) },
            { "_InstantiateObject", (512, 128) },
            { "_SendOnSpawn", (512, 128) },
            { "ConfigurePortal", (512, 128) },
            { "UdonSyncRunProgramAsRPC", (512, 128) }, // <--- Udon is gay
            { "ChangeVisibility", (128, 12) },
            { "PhotoCapture", (128, 32) },
            { "TimerBloop", (128, 16) },
            { "ReloadAvatarNetworkedRPC", (128, 12) },
            { "InternalApplyOverrideRPC", (512, 128) },
            { "AddURL", (64, 6) },
            { "Play", (64, 6) },
            { "Pause", (64, 6) },
            { "SendVoiceSetupToPlayerRPC", (512, 6) },
            { "SendStrokeRPC", (512, 32) }
        };
        public static bool Log = false;
        public static void init()
        {
            foreach (var kv in rpcs)
            {
                Utility.Logger.log("Patching rpc " + kv.Key);
                Classes.Daydream.daydreampatch.Patch(typeof(VRC_EventDispatcherRFC)
                    .GetMethod(nameof(VRC_EventDispatcherRFC.Method_Public_Void_Player_VrcEvent_VrcBroadcastType_Int32_Single_0), BindingFlags.Public | BindingFlags.Instance),
                    new HarmonyMethod(typeof(RpcLog).GetMethod(nameof(RPC_RECIEVED), BindingFlags.Public | BindingFlags.Static)));
            }
            // Networking.rpc
            //PhotonServerSettings.
            
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
        private static string PrintObj(Il2CppReferenceArray<GameObject> bytes)
        {
            try
            {
                if (bytes.Count > 0)
                {
                    var sb = new StringBuilder("new[]{ ");
                    foreach (GameObject b in bytes)
                    {
                        sb.Append(b.name + ", ");
                    }
                    sb.Append("}");
                    return sb.ToString();
                }
                else
                {
                    return "new[]{ }";

                }
            }
            catch
            {
                return "new[]{ }";

            }

        }
        public static void RPC_RECIEVED(VRC.Player param_1, VRC_EventHandler.VrcEvent param_2, VRC_EventHandler.VrcBroadcastType param_3, int param_4, float param_5)
        {
            if(Log == true)
            {
                Utility.Logger.log("Log RPC");
                Utility.Logger.log("***************************************************");
                Utility.Logger.log("Player: " + param_1.name);
                Utility.Logger.log("Pointer: " + param_2.Pointer);
                Utility.Logger.log("Event Name: " + param_2.ParameterString);
                Utility.Logger.log("Event Type: " + param_2.EventType);
                Utility.Logger.log("Object: " + param_2.ParameterObject.name);
                Utility.Logger.log("Bytes: " + PrintByteArray(param_2.ParameterBytes));
                Utility.Logger.log("Broadcast type " + param_3);
                Utility.Logger.log("param_4 " + param_4);
                Utility.Logger.log("param_5: " + param_5);
                Utility.Logger.log("***************************************************");
            }
        }
    }
}

