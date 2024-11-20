using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC;

namespace Daydream.Client.Classes
{
    internal class DaydreamPlayers
    {
        public static void OnPlayerJoined(VRC.Player player)
        {
            Utility.Logger.log("Player " + player.field_Private_APIUser_0.displayName + " joined");
            
            if (player.prop_APIUser_0.displayName != VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_VRCPlayerApi_0.displayName)
            {
                string data;
                if (Classes.Daydream.isphotonbot == true)
                {
                    data = "DaydreamJoinBot";
                }
                else
                {
                    data = "DaydreamJoin";
                }
                Communication.DaydreamNetwork.Send(player.prop_APIUser_0.id, data);
                Hacks.playerinfo.PlayerJoin(player);
            }
            Classes.Daydream.discord.Update();
        }
        public static void OnPlayerLeft(VRC.Player player)
        {
            Utility.Logger.log("Player " + player.field_Private_APIUser_0.displayName + " left");
            Hacks.playerinfo.PlayerLeave(player);

        }
    }
}
