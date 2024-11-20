using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.DataModel.Core;

namespace Daydream.Client.Hacks
{
    internal class ForceClone
    {
        public static void allowclone()
        {
            foreach (VRC.Player plr in Utility.Extentions.GetPlayers())
            {
                if (plr.field_Private_VRCPlayerApi_0.displayName.ToString() == Ui.TabUi.getselectedplayer().displayName.ToString())
                {
                    Utility.Extentions.ChangeToAvatar(VRCPlayer.field_Internal_Static_VRCPlayer_0, plr.prop_ApiAvatar_0.id);
                }
            }
        }
    }
}
