using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC;
using VRC.DataModel;

namespace Daydream.Client.Hacks
{
    internal class tpto
    {
        public static void tptoselected()
        {
            Player player = PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0.ToArray().FirstOrDefault((Player a) => a.field_Private_APIUser_0 != null && a.field_Private_APIUser_0.id == UserSelectionManager.field_Private_Static_UserSelectionManager_0.field_Private_APIUser_1.id); VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.TeleportTo(player.transform.position, player.transform.rotation);
            VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.TeleportTo(player.transform.position, player.transform.rotation);
            //  Classes.Daydream.localplayer.transform.position = VRChatUtilityKit.Utilities.VRCUtils.ActivePlayerInUserInfoMenu.prop_VRCPlayerApi_0.GetPosition();
        }
    }
}
