
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExitGames.Client.Photon;
using Il2CppSystem;
using MelonLoader;
using Photon.Realtime;
using UnityEngine;
using VRC.SDKBase;





namespace Daydream.Client.Hacks
{

    internal class event6
    {
        // FOR SOME REASON CANNOT BE SEARIALISED
        private static bool enabled = false;
        public static void enable(bool val)
        {
            Classes.Daydream.HackUpdate("Event6 Lagger",val);
            enabled = val;
            MelonCoroutines.Start(run());
        }
        private static IEnumerator run()
        {
            // byte[] rpcData =
            // {
            //  106, 191, 218, 132, 88, 12, 0, 0, 0, 7,
            //   0, 58, 118, 213, 255, 255, 50, 47, 14, 0,
            //   255, 0, 0, 0, 0, 9, 0, 0, 0, 13,
            //   0, 69, 110, 97, 98, 108, 101, 77, 101, 115,
            //   104, 82, 80, 67, 0, 0, 0, 0, 0, 0,
            //    0, 2
            //  };
            //
            byte[] NewData = { 106, 162, 57, 181, byte.MaxValue, 2, 66, 142, 63, 7, 52, 59, 51, 38, 78, 102, byte.MaxValue, 44, 74, 0, 100, 97, 32, byte.MaxValue,  98, 63, 23, 0, 0, byte.MaxValue, 255, 83, 112, 97, 179, 110, 69, 109, 144, byte.MaxValue, 105, 82, 80, 67, 0, byte.MaxValue, 0, 0, 4, 8, 0, 21, 110, 1, 0, byte.MaxValue, 1, 44, 0, 32, };
            //System.Buffer.BlockCopy(Il2CppSystem.BitConverter.GetBytes(VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_VRCPlayerApi_0.playerId), 0, NewData, 5, 4);
            for (; ; )
            {
                if (enabled == true)
                {
                    for (int i = 0; i < 80; i++)
                    {
                        Hacks.PhotonExtentions.OpRaiseEvent(6, NewData, new RaiseEventOptions
                        {
                            field_Public_ReceiverGroup_0 = Photon.Realtime.ReceiverGroup.Others,
                            field_Public_EventCaching_0 = Photon.Realtime.EventCaching.DoNotCache,
                        }, default);
                        Hacks.PhotonExtentions.OpRaiseEvent(6, NewData, new RaiseEventOptions
                        {
                            field_Public_ReceiverGroup_0 = Photon.Realtime.ReceiverGroup.Others,
                            field_Public_EventCaching_0 = Photon.Realtime.EventCaching.DoNotCache,
                        }, default);
                        Hacks.PhotonExtentions.OpRaiseEvent(6, NewData, new RaiseEventOptions
                        {
                            field_Public_ReceiverGroup_0 = Photon.Realtime.ReceiverGroup.Others,
                            field_Public_EventCaching_0 = Photon.Realtime.EventCaching.DoNotCache,
                        }, default);
                    }
                    yield return new WaitForSeconds(0.01f);
                }
                else
                {
                    yield break;
                }
            }
        }
    }
}
