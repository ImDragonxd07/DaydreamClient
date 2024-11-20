using System.Collections;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;
using MelonLoader;

namespace Daydream.Client.Hacks
{
    internal class ItemStorm
    {
        private static bool enabled = false;
        private static IEnumerator start()
        {
            Random rd = new Random();
            for (; ; )
            {
                yield return new WaitForEndOfFrame();
                if (enabled == false)
                {
                    yield break;
                }
                GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
                foreach (GameObject go in allObjects)
                {
                    if (go.GetComponent<VRCPickup>())
                    {
                        Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_VRCPlayerApi_0, go.gameObject);
                        go.GetComponent<Rigidbody>().isKinematic = false;
                        go.transform.position = Camera.main.transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-2, 2), Random.Range(-10, 10));

                        Networking.SetOwner(null, go.gameObject);
                    }
                }
            }
        }
        public static void enable(bool val)
        {
            enabled = val;
            Classes.Daydream.HackUpdate("Item Storm", enabled);
            MelonCoroutines.Start(start());
        }
    }
   
}
