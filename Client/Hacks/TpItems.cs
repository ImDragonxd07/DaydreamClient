using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;

namespace Daydream.Client.Hacks
{
    internal class TpItems
    {
        public static void update()
        {
            Classes.Daydream.HackUpdate("TpItems", true);
            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (GameObject go in allObjects)
            {
                if (go.GetComponent<VRCPickup>())
                {
                    Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_VRCPlayerApi_0, go.gameObject);
                    go.transform.position = Classes.Daydream.localplayer.transform.position;
                }
            }
            Classes.Daydream.HackUpdate("TpItems", false);
        }
    }
}
