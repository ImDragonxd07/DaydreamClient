using ExitGames.Client.Photon;
using Daydream.Client.Utility.Serialization;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daydream.Client.Hacks
{
    internal static class PhotonExtentions
    {
		public static void OpRaiseEvent(byte code, object customObject, RaiseEventOptions RaiseEventOptions, SendOptions sendOptions)
		{
			Object Object = Serialization.FromManagedToIL2CPP<Il2CppSystem.Object>(customObject);
			PhotonExtentions.step2(code, Object, RaiseEventOptions, sendOptions);
		}

		private static void step2(byte code, Object customObject, RaiseEventOptions RaiseEventOptions, SendOptions sendOptions)
		{
			PhotonNetwork.Method_Private_Static_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0(code, (Il2CppSystem.Object)customObject, RaiseEventOptions, sendOptions);
		}
	}
}
