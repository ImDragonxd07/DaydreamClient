using System.Collections;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;
using MelonLoader;

namespace Daydream.Client.Hacks
{
    internal class ItemSwastica
    {
        private static bool iseven(int num)
        {
            return !(num % 2 == 0);
        }
        private static bool enabled = false;
        public static Vector3[] positions = {
                    new Vector3(0, 0, 0),
                    new Vector3(0, 0.5f, 0), //top --2
                    new Vector3(0, 1f, 0), // top
                    new Vector3(0.5f, 1f, 0), // top
                    new Vector3(1f,1f,0), // top --5

                    new Vector3(0.5f, 0, 0), // right --6
                    new Vector3(1f, 0, 0), // right
                    new Vector3(1f, -0.5f, 0), // right
                    new Vector3(1f,-1f,0), // right --9

                    new Vector3(0, -0.5f, 0), // lower --10
                    new Vector3(0, -1f, 0), // lower
                    new Vector3(-0.5f, -1f, 0), // lower
                    new Vector3(-1f, -1f, 0), // lower --13

                    new Vector3(-0.5f, 0, 0),// left --14
                    new Vector3(-1f, 0, 0),// left
                    new Vector3(-1f, 0.5f, 0),// left
                    new Vector3(-1f, 1f, 0),// left --17
                    };
        private static IEnumerator start()
        {
            for (; ; )
            {
                yield return new WaitForEndOfFrame();
                if(enabled == false)
                {
                    yield break;
                }
                Vector3 basepos = Camera.main.transform.position + new Vector3(0, 1.5f, 0);
                int num = 0;
                GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
                foreach (GameObject go in allObjects)
                {
                    if (go.GetComponent<VRCPickup>())
                    {
                        Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_VRCPlayerApi_0, go.gameObject);
                        go.GetComponent<Rigidbody>().isKinematic = true;
                        go.transform.position = basepos + positions[num];
                        num += 1;
                        if (num >= positions.Length)
                        {
                            num = 0;
                        }
                    }
                }
            }
        }
        public static void enable(bool val)
        {
            enabled = val;
            Classes.Daydream.HackUpdate("Item Swastika", enabled);
            MelonCoroutines.Start(start());
        }
    }
}
