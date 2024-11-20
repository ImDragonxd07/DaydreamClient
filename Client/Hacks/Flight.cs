using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.SDKBase;

namespace Daydream.Client.Hacks
{
    internal class Flight
    {
		public static int Speed = 1;
		public static bool FlightEnabled = false;
		public static void Enable(bool val)
        {
			Hacks.Noclip.enabled(val);
			Classes.Daydream.HackUpdate("Fly",val);
			Update(val);
        }
		private static void Update(bool val)
		{
			FlightEnabled = val;
			if (val == true)
            {
				Classes.Daydream.Impulse = Networking.LocalPlayer.GetJumpImpulse();
			}
			MelonCoroutines.Start(Flight.Loop());
		}
		// Token: 0x0600025E RID: 606 RVA: 0x00018493 File Offset: 0x00016693
		public static IEnumerator Loop()
		{
			for (; ; )
			{
				if (FlightEnabled == true)
				{
                    Networking.LocalPlayer.SetJumpImpulse(0f);
					Classes.Daydream.localplayer.GetComponent<Rigidbody>().useGravity = false;
					if (Input.GetAxis("Vertical") != 0f)
					{
						Classes.Daydream.localplayer.transform.position += Classes.Daydream.localplayer.transform.forward * Time.deltaTime * (Input.GetAxis("Vertical")) * (false ? 1 : 1f) * (Input.GetKey(KeyCode.LeftShift) ? 4f : 2f); //+ VRCInputManager.field_Private_Static_Dictionary_2_String_VRCInput_0["Horizontal"].field_Public_Single_0
                    }
					if (Input.GetAxis("Horizontal") != 0f)
					{
						Classes.Daydream.localplayer.transform.position += Classes.Daydream.localplayer.transform.right * Time.deltaTime * (Input.GetAxis("Horizontal") ) * (false ? 1 : 1f) * (Input.GetKey(KeyCode.LeftShift) ? 4f : 2f); //+ VRCInputManager.field_Private_Static_Dictionary_2_String_VRCInput_0["Horizontal"].field_Public_Single_0
                    }
					if (Input.GetKey(KeyCode.Q))
					{
						Classes.Daydream.localplayer.transform.position -= new Vector3(0f, Time.deltaTime * (Input.GetKey(KeyCode.LeftShift) ? 4f : 2f) * (false ? 1 : 1f), 0f);
					}
					if (Input.GetKey(KeyCode.E))
					{
						Classes.Daydream.localplayer.transform.position += new Vector3(0f, Time.deltaTime * (Input.GetKey(KeyCode.LeftShift) ? 4f : 2f) * (false ? 1 : 1f), 0f);
					}
					yield return new WaitForEndOfFrame();
				}
				else
                {
                    Networking.LocalPlayer.SetJumpImpulse(Classes.Daydream.Impulse);
					Classes.Daydream.localplayer.GetComponent<Rigidbody>().useGravity = true;
					yield break;
				}
			}
		}

		// Token: 0x0400038F RID: 911


		// Token: 0x04000391 RID: 913

		// Token: 0x04000395 RID: 917
	}
}
