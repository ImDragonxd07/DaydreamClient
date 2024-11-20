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
    internal class Spaceitems
    {
        // WIP
        private static Vector3 gravity;
        public static void enable()
        {
            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (GameObject go in allObjects)
            {
                if (go.GetComponent<VRCPickup>())
                {
                    Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_VRCPlayerApi_0, go.gameObject);
                    go.GetComponent<Rigidbody>().useGravity = false;
                }
            }

        }
    }
}
