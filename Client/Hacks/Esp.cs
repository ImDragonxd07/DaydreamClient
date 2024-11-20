using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Daydream.Client.Hacks
{
    internal class Esp
    {
		public static void Enable(bool val)
        {
			Classes.Daydream.HackUpdate("PlayerEsp", val);
			ESPEnabled = val;
			MelonCoroutines.Start(Esp.Loop());
		}
		private static IEnumerator Loop()
		{
			for (; ; )
			{
				if (ESPEnabled == true)
				{
					foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Player"))
					{
						if (gameObject.transform.Find("SelectRegion"))
						{
							HighlightsFX.field_Private_Static_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(gameObject.transform.Find("SelectRegion").GetComponent<Renderer>(), true);
						}
					}
					yield return new WaitForEndOfFrame();

				}
				else
				{
					foreach (GameObject gameObject2 in GameObject.FindGameObjectsWithTag("Player"))
					{
						if (gameObject2.transform.Find("SelectRegion"))
						{
							HighlightsFX.field_Private_Static_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(gameObject2.transform.Find("SelectRegion").GetComponent<Renderer>(), false);
						}
					}
					yield break;
				}
			}
		}
		// Token: 0x04000380 RID: 896
		public static bool ESPEnabled;

		// Token: 0x04000381 RID: 897
		private static bool wasEnabled;
	}
}
