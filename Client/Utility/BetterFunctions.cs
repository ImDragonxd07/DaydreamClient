using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Daydream.Client.Utility
{
    internal class BetterFunctions
    {
        public static IEnumerable waitforplayer()
        {
            while (!VRCPlayer.field_Internal_Static_VRCPlayer_0.gameObject)
            {

            }
            yield return new WaitForSeconds(1f);
        }
        public static IEnumerator DelayAction(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);
            action();
            yield break;
        }
    }
}
