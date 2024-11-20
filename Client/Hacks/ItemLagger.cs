using System.Collections;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;
using MelonLoader;

namespace Daydream.Client.Hacks
{
    internal class ItemLagger
    {
        private static bool enabled = false;
        private static IEnumerator start()
        {
            for (; ; )
            {
                yield return new WaitForEndOfFrame();
                if (enabled == false)
                {
                    yield break;
                }
                foreach (VRC.SDKBase.VRC_Trigger vrc_Trigger in UnityEngine.Object.FindObjectsOfType<VRC.SDKBase.VRC_Trigger>())
                {
                    bool hasPickupTriggers = vrc_Trigger.HasPickupTriggers;
                    if (hasPickupTriggers)
                    {
                        vrc_Trigger.TakesOwnershipIfNecessary.ToString();
                        vrc_Trigger.Interact();
                    }
                }
                foreach (VRC.SDKBase.VRC_Pickup vrc_Pickup in UnityEngine.Object.FindObjectsOfType<VRC.SDKBase.VRC_Pickup>())
                {
                    bool flag = vrc_Pickup.GetComponent<Collider>() && !vrc_Pickup.GetComponent<VRC.SDKBase.VRC_SpecialLayer>() && !vrc_Pickup.IsHeld;
                    if (flag)
                    {
                        Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_VRCPlayerApi_0, vrc_Pickup.gameObject);
                        vrc_Pickup.pickupable = true;
                        vrc_Pickup.ThrowVelocityBoostMinSpeed = int.MaxValue;
                        vrc_Pickup.ThrowVelocityBoostScale = int.MaxValue;
                        vrc_Pickup.transform.position = new Vector3(int.MaxValue, int.MaxValue, int.MaxValue);
                    }
                }
                yield return new WaitForSeconds(2f);
                foreach (VRC.SDKBase.VRC_Pickup vrc_Pickup in UnityEngine.Object.FindObjectsOfType<VRC.SDKBase.VRC_Pickup>())
                {
                    bool flag = vrc_Pickup.GetComponent<Collider>() && !vrc_Pickup.GetComponent<VRC.SDKBase.VRC_SpecialLayer>() && !vrc_Pickup.IsHeld;
                    if (flag)
                    {
                        Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_VRCPlayerApi_0, vrc_Pickup.gameObject);
                        vrc_Pickup.pickupable = true;
                        vrc_Pickup.ThrowVelocityBoostMinSpeed = int.MaxValue;
                        vrc_Pickup.ThrowVelocityBoostScale = int.MaxValue;
                        vrc_Pickup.transform.position = Classes.Daydream.localplayer.transform.position;
                    }
                }
                yield return new WaitForSeconds(2f);
            }
            yield break;
        }
        public static void enable(bool val)
        {
            enabled = val;
            Classes.Daydream.HackUpdate("Item Lagger", enabled);
            MelonCoroutines.Start(start());
        }
    }
   
}
