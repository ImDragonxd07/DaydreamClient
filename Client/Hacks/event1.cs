using MelonLoader;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.SDKBase;

namespace Daydream.Client.Hacks
{
    [Serializable]

    internal class event1
    {
        private static bool enabled = false;
        public static void enable(bool val)
        {
            Classes.Daydream.HackUpdate("Event1 Lagger", val);
            enabled = val;
            MelonCoroutines.Start(run());
        }
        private static IEnumerator run()
        {

            for (; ; )
            {
                if (enabled == false)
                {
                    yield break;
                }

                byte[] VoiceData = Convert.FromBase64String(
            "AAAAAAAAAAC7hjsA+H3owFygUv4w5B67lcSx14zff9FCPADiNbSwYWgE+O7Dhiy5tkRecs21ljjofvebe6xsYlA4cVmght0=");
                byte[] nulldata = new byte[4];
                byte[] ServerTime = BitConverter.GetBytes(Networking.GetServerTimeInMilliseconds());
                Buffer.BlockCopy(nulldata, 0, VoiceData, 0, 4);
                Buffer.BlockCopy(ServerTime, 0, VoiceData, 4, 4);
                int num;
                for (int i = 0; i < 80; i = num + 1)
                {
                    if(enabled == false)
                    {
                        yield break;
                    }
                    Hacks.PhotonExtentions.OpRaiseEvent(1, VoiceData, new RaiseEventOptions
                    {
                        field_Public_ReceiverGroup_0 = 0,
                        field_Public_EventCaching_0 = 0
                    }, default);
                    num = i;
                }
                VoiceData = null;
                nulldata = null;
                ServerTime = null;
                yield return new WaitForSeconds(0.01f);
            }
        }

    }
}
