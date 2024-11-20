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

namespace Daydream.Client.Hacks
{
    internal class MasterDC
    {
        private static bool enabled = false;
        private static IEnumerator start()
        {
            for (; ; )
            {
                yield return new WaitForEndOfFrame();
                if (enabled == false)
                {
                    yield break;
                }


            }
        }
            public static void enable(bool val)
        {
            enabled = val;
            Client.Classes.Daydream.HackUpdate("Lag Server", val);
            MelonCoroutines.Start(start());
        }
    }
}
