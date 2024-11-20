using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.SDKBase;

namespace Daydream.Client.Utility
{
    internal class PortalManager
    {
        public static string RandomNumberString(int length)
        {
            string str = "";
            for (int index = 0; index < length; ++index)
                str += new System.Random().Next(0, 9).ToString();
            return str;
        }
        private static bool isspamming = false;
        private static GameObject lastportal;

        public static void CustomPortal(VRCPlayer plr, string wrldid, float time)
        {

            Utility.Logger.log("Created Portal");
            GameObject targetObject = Networking.Instantiate(0, "Portals/PortalInternalDynamic", plr.gameObject.transform.position + plr.gameObject.transform.forward * 1.4f, Quaternion.Euler(0f, 0f, 0f));
            Utility.Logger.log("Creating portal " + targetObject.name + " At " + targetObject.transform.position.ToString() + " Selected Player Postion " + plr.gameObject.transform.position.ToString());
            Networking.RPC(RPC.Destination.AllBufferOne, targetObject, "ConfigurePortal", new Il2CppSystem.Object[]
            {
                        (Il2CppSystem.String)wrldid,
                        (Il2CppSystem.String)"101110",
                        new Il2CppSystem.Int32
                    {
                      m_value = new System.Random().Next(int.MinValue, int.MaxValue)
                    }.BoxIl2CppObject()
            });
            targetObject.transform.GetComponent<PortalInternal>().SetTimerRPC(time, VRC.Player.prop_Player_0);

            Utility.Logger.log("Created Portal");
        }

        public static void dropportal(VRCPlayer plr, string wrldid)
        {

            Utility.Logger.log("Created Portal");
            GameObject targetObject = Networking.Instantiate(0, "Portals/PortalInternalDynamic", plr.gameObject.transform.position + plr.gameObject.transform.forward * 1.4f, Quaternion.Euler(0f, 0f, 0f));
            Utility.Logger.log("Creating portal " + targetObject.name + " At " + targetObject.transform.position.ToString() + " Selected Player Postion " + plr.gameObject.transform.position.ToString());
            Networking.RPC(RPC.Destination.AllBufferOne, targetObject, "ConfigurePortal", new Il2CppSystem.Object[]
            {
                        (Il2CppSystem.String)wrldid,
                        (Il2CppSystem.String)"101110",
                        new Il2CppSystem.Int32
                    {
                      m_value = new System.Random().Next(int.MinValue, int.MaxValue)
                    }.BoxIl2CppObject()
            });
            Utility.Logger.log("Created Portal");
        }

        private static IEnumerator portal(VRCPlayer plr)
        {
            isspamming = true;
            while (isspamming == true)
            {

                Utility.Logger.log("Created Portal");
                GameObject targetObject = Networking.Instantiate(0, "Portals/PortalInternalDynamic", plr.gameObject.transform.position + plr.gameObject.transform.forward * 1.4f, Quaternion.Euler(0f, 0f, 0f));
                Utility.Logger.log("Creating portal " + targetObject.name + " At " + targetObject.transform.position.ToString() + " Selected Player Postion " + plr.gameObject.transform.position.ToString());
                Networking.RPC(RPC.Destination.AllBufferOne, targetObject, "ConfigurePortal", new Il2CppSystem.Object[]
                {
                        (Il2CppSystem.String)"wrld_d29bb0d0-d268-42dc-8365-926f9d485505",
                        (Il2CppSystem.String)"101110",
                        new Il2CppSystem.Int32
                    {
                      m_value = new System.Random().Next(int.MinValue, int.MaxValue)
                    }.BoxIl2CppObject()
                });
                lastportal = targetObject;
                Utility.Logger.log("Created Portal");
                yield return (object)new WaitForSeconds(4f);
            }
            isspamming = false;
            Utility.Logger.log("Ended Portal Spam");
        }
        public static void dropportalonselected()
        {
            if (Ui.TabUi.getselectedplayer() != null)
            {
                foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Player"))
                {
                    if (gameObject.GetComponent<VRCPlayer>().prop_VRCPlayerApi_0.displayName.ToString() == Ui.TabUi.getselectedplayer().displayName.ToString())
                    {
                        Utility.Logger.log("Found Player " + gameObject.transform.name);
                        MelonCoroutines.Start(portal(gameObject.GetComponent<VRCPlayer>()));
                    }
                }
            }
        }
    }
}
