using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TextCore;
using VRC.SDKBase;

namespace Daydream.Client.Hacks
{
    internal class Noclip
    {
        public static bool UsingNoclip = false;
        public static void enabled(bool val)
        {
            UsingNoclip = val;
            Classes.Daydream.HackUpdate("NoClip", val);
            // Classes.Daydream.localplayer.GetComponent<VRCMotionState>().field_Private_CharacterController_0.enabled = !val;
            Networking.LocalPlayer.gameObject.GetComponent<CharacterController>().enabled = !val;
        }
    }
}
