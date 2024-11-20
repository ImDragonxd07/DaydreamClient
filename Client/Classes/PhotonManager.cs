using ExitGames.Client.Photon;
using Harmony;
using Il2CppNewtonsoft.Json;
using MelonLoader;
using Daydream.Client.Protections.PhotonClient;
using Daydream.Client.Utility.Serialization;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC;
using VRC.Networking;
using Il2CppSystem.Collections.Generic;
using System.Collections;
using UnityEngine.Networking;
using System.Net;

namespace Daydream.Client.Classes
{

    public class PhotonManager
    {
        private static IEnumerator waitfornetworkmanager()
        {
            yield return new WaitUntil((Func<bool>)(() => NetworkManager.field_Internal_Static_NetworkManager_0 != null));
            Utility.Logger.log("Found networkmanager");
            var playerJoinedDelegate = NetworkManager.field_Internal_Static_NetworkManager_0.field_Internal_VRCEventDelegate_1_Player_0;
            var playerLeftDelegate = NetworkManager.field_Internal_Static_NetworkManager_0.field_Internal_VRCEventDelegate_1_Player_1;
            Utility.Logger.log("Loading vrc events");

            playerJoinedDelegate.field_Private_HashSet_1_UnityAction_1_T_0.Add(new Action<VRC.Player>(p =>
            {
                if (p != null) OnPlayerJoined(p);
            }));

            playerLeftDelegate.field_Private_HashSet_1_UnityAction_1_T_0.Add(new Action<VRC.Player>(p =>
            {
                if (p != null) OnPlayerLeft(p);
            }));
        }
        public static void onclientload()
        {


            Classes.Daydream.daydreampatch.Patch(typeof(LoadBalancingClient).GetMethod(nameof(LoadBalancingClient.OnEvent)), new HarmonyMethod(typeof(AntiCrash), "OnEvent"));

            Utility.Logger.log("Installed custom events");

            if(NetworkManager.field_Internal_Static_NetworkManager_0 == null)
            {
                Utility.Logger.errorlog("Missing networkmanager");
            }
            MelonCoroutines.Start(waitfornetworkmanager());
            Utility.Logger.log("Installed vrc events");
            
            //patched.Patch(typeof(FlatBufferNetworkSerializer).GetMethod(nameof(FlatBufferNetworkSerializer.Method_Public_Void_EventData_0)), typeof(AntiCrash).GetMethod("OnEvent").ToNewHarmonyMethod());
        }

        public static void OnPlayerJoined(VRC.Player player)
        {
            if(Classes.Daydream.firstjoin == false)
            {
                Classes.Daydream.onfirstjoin();
            }
            DaydreamPlayers.OnPlayerJoined(player);
            Protections.PhotonClient.AntiCrash.OnPlayerJoined(player);
        }
         public static void OnPlayerLeft(VRC.Player player)
        {
            DaydreamPlayers.OnPlayerLeft(player);
            Protections.PhotonClient.AntiCrash.OnPlayerLeft(player);

        }
        private static bool OnOPRaiseEvent(byte __0, ref Il2CppSystem.Object __1, ref RaiseEventOptions __2, ref SendOptions __3)
        {
            try
            {

                Utility.Logger.log($"[OPRaiseEvent {__0}]");
                Utility.Logger.log($"-------------------");
                Utility.Logger.log("RaiseEventOptions:");
                Utility.Logger.log($"Payload: {JsonConvert.SerializeObject((Il2CppSystem.Object)Serialization.FromIL2CPPToManaged<object>(__1))}");
                Utility.Logger.log($"Caching Type: {__2.field_Public_EventCaching_0}");
                Utility.Logger.log($"Receiver Group: {__2.field_Public_ReceiverGroup_0}");
                Utility.Logger.log($"Target Actors {__2.field_Public_ArrayOf_Int32_0}");
                Utility.Logger.log($"Unknown Byte 1: {__2.field_Public_Byte_0}");
                Utility.Logger.log($"Unknown Byte 2: {__2.field_Public_Byte_1}");
                Utility.Logger.log($"Webflag byte: {__2.field_Public_WebFlags_0.field_Public_Byte_0}");
                Utility.Logger.log($"-------------------");
                Utility.Logger.log($"SendOptions:");
                Utility.Logger.log($"Channel: {__3.Channel}");
                Utility.Logger.log($"Delivery Mode: {__3.DeliveryMode}");
                Utility.Logger.log($"Encrypt: {__3.Encrypt}");
                Utility.Logger.log($"Reliable?: {__3.Reliability}");
                Utility.Logger.log($"-------------------");
            }
            catch // this is caused 99.99% of the time because of the event containing something i can't grab (like the payload)
            {
            }

            return true;
        }
    }
}
