using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC;

namespace Daydream.Client.Hacks
{
    internal class Orbit
    {
		internal static float NextFloat(float min, float max)
		{
			return (float)(new System.Random().NextDouble() * (double)(max - min) + (double)min);
		}
		public static float OrbitSpeed = 13f;

		// Token: 0x0400032E RID: 814
		public static float alpha = 0f;

		// Token: 0x0400032F RID: 815
		public static float b = 1f;

		// Token: 0x04000330 RID: 816
		public static float a = 1f;
		public static bool orbitenabled = false;
		public static IEnumerator OrbitEnu(Player P)
		{
			Utility.Logger.log("orbit enum");

			if (P == null)
			{
				Utility.Logger.errorlog("Cant find player");
			}
			for (; ; )
			{

				if (orbitenabled == true)
				{
					//Classes.Daydream.localplayer.GetComponent<Rigidbody>().useGravity = false;
					Utility.Logger.log("orbit update");

					alpha += Time.deltaTime * NextFloat(OrbitSpeed, OrbitSpeed - 1.3f);
					Utility.Logger.log("Get Pos");

					Vector3 pos = new Vector3(P.prop_VRCPlayerApi_0.GetPosition().x + a * (float)Math.Cos((double)alpha), P.prop_VRCPlayerApi_0.GetPosition().y, P.prop_VRCPlayerApi_0.GetPosition().z + b * (float)Math.Sin((double)alpha));
					Utility.Logger.log("new pos " + pos.x + "," + pos.y + "," + pos.z);

					VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position = pos;
				}
				else
				{
					//Classes.Daydream.localplayer.GetComponent<Rigidbody>().useGravity = true;
					yield break;

				}
				yield return new WaitForSeconds(0.19f);

			}
		}
	}
}
