
using UnityEngine;
using VRC.Udon;
using Il2CppSystem;
using Il2CppSystem.Collections.Generic;
using System.Collections;

namespace Daydream.Client.Classes
{
    internal class UdonManager
    {
        public static UdonBehaviour[] GetUdonBehaviours()
        {
            return Resources.FindObjectsOfTypeAll<UdonBehaviour>();
        }


        public static List<string> GetEventNames(UdonBehaviour ub)
        {
            List<string> events = new List<string>();

            foreach (var e in ub._eventTable)
            {
                events.Add(e.key);
            }

            return events;
        }
        public static void triggerevent(string even)
        {
            foreach (var udonEvent in GameObject.FindObjectsOfType<UdonBehaviour>())
            {
                Utility.Logger.log("Send event " + even);
                udonEvent.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, even);
            }
        }
        public static IEnumerator triggerall()
        {
            foreach (var e in Classes.UdonManager.GetUdonBehaviours())
            {
                foreach (var objet in Classes.UdonManager.GetEventNames(e))
                {
                    Utility.Logger.log("Send event");
                    Classes.UdonManager.triggerevent(objet);
                    yield return new WaitForSeconds(0.05f);
                }
            }
        }
    }
    
}
