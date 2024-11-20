using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using VRC;

namespace Daydream.Client.Hacks
{

    internal static class playerinfo
    {
        public static bool showplayerinfo = false;

        public static int GetFps(Player player)
        {
            if (player._playerNet.field_Private_Byte_0 == 0)
            {
                return -1;
            }
            return (int)Mathf.Floor(1000f / player._playerNet.field_Private_Byte_0);
        }
        public static int GetPing(Player player) 
        {
            if (player._playerNet.field_Private_Byte_1 == 0)
            {
                return -1;
            }
            return (int)Mathf.Clamp(player.prop_VRCPlayer_0.prop_Int16_0, -999, 9999);
        }
        public  static List<Utility.Nameplate> plrs = new List<Utility.Nameplate>();
        public static void PlayerJoin(VRC.Player player)
        {

            var plrnameplate = new Utility.Nameplate(player,"",Color.white);
            plrnameplate.GameObject.GetComponent<ImageThreeSlice>().enabled = false;
            plrnameplate.Position = new Vector3(0,25,0);
            plrs.Add(plrnameplate);
        }
        public static void PlayerLeave(VRC.Player player)
        {
            foreach(var nameplate in plrs)
            {
                if(nameplate.Player.name == player.name)
                {
                    GameObject.Destroy(nameplate.GameObject);
                    plrs.Remove(nameplate);
                }
            }

        }
        private static bool ClientDetect(this Player player)
        {
            return GetFps(player) > 100f || GetFps(player) < 10f || GetPing(player) > 777 || GetPing(player) < 22;
        }
        private static string addcatagory(string str)
        {
            return " | " + str;
        }
        private static string GetPlatformShort(this Player player)
        {
            if (player.field_Private_APIUser_0.IsOnMobile)
            {
                return "Quest";
            }
            if (player.field_Private_VRCPlayerApi_0.IsUserInVR())
            {
                return "PCVR";
            }
            return "PC";
        }
        private static Gradient dynamicFPSColourGradient;
        private static Gradient dynamicPingColourGradient;
        public static void Start()
        {
            var colKey = new GradientColorKey[3];
            colKey[0].color = Color.red;
            colKey[0].time = 0.0f;
            colKey[1].color = Color.yellow;
            colKey[1].time = 0.5f;
            colKey[2].color = Color.green;
            colKey[2].time = 1f;

            var alphaKey = new GradientAlphaKey[2];
            alphaKey[0].alpha = 1.0f;
            alphaKey[0].time = 0f;
            alphaKey[1].alpha = 1.0f;
            alphaKey[1].time = 1f;

            dynamicFPSColourGradient = new Gradient { colorKeys = colKey, alphaKeys = alphaKey, mode = GradientMode.Blend };

            var colKeyPing = new GradientColorKey[4];
            colKeyPing[0].color = Color.green;
            colKeyPing[0].time = 0.0f;
            colKeyPing[1].color = Color.green;
            colKeyPing[1].time = 0.25f;
            colKeyPing[2].color = Color.yellow;
            colKeyPing[2].time = 0.5f;
            colKeyPing[3].color = Color.red;
            colKeyPing[3].time = 1f;

            dynamicPingColourGradient = new Gradient { colorKeys = colKeyPing, alphaKeys = alphaKey, mode = GradientMode.Blend };
        }
        public static void UpdateNameplates()
        {
            if (showplayerinfo == true)
            {
                foreach (var nameplate in plrs)
                {
                    nameplate.GameObject.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
                    nameplate.Text = "";
                    nameplate.Text += addcatagory(GetPlatformShort(nameplate.Player));
                    nameplate.Text += addcatagory($"<color=#{ColorUtility.ToHtmlStringRGB(dynamicFPSColourGradient.Evaluate((float)GetFps(nameplate.Player) / 60))}> {GetFps(nameplate.Player).ToString() + " FPS"} </color> ");
                    nameplate.Text += addcatagory($"<color=#{ColorUtility.ToHtmlStringRGB(dynamicPingColourGradient.Evaluate((float)GetPing(nameplate.Player) / 300))}> {GetPing(nameplate.Player).ToString() + " MS"} </color> ");
                    //<color=#FF0000> blablabla </color>
                    if (nameplate.Player.field_Private_VRCPlayerApi_0.isMaster)
                    {
                        nameplate.Text += addcatagory("<color=#FFC400> Master </color>");
                    }
                    if (ClientDetect(nameplate.Player))
                    {
                        nameplate.Text += addcatagory("Other Client User");
                    }
                    nameplate.Text += " | "; // add line to close off string (just for looks)
                }
            }
            else
            {
                foreach (var nameplate in plrs)
                {
                    nameplate.GameObject.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
                }
            }
        }

    }
}
