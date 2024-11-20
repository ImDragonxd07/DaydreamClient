using MelonLoader;
using MelonLoader.TinyJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

namespace Daydream.Client.Ui.Pages
{
    internal class Changelog
    {
        public static string txt = "Error";
        private static VRChatUtilityKit.Ui.SubMenu page;
        private static IEnumerator git()
        {
            page = Client.Ui.TabUi.createuipage("Changelog");

            UnityWebRequest www = UnityWebRequest.Get("https://api.github.com/repos/GITDragonxd07/UnityClientDll/releases/latest");
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                MelonLogger.Log(www.error);
            }
            else
            {
                var Data = JSON.Load(www.downloadHandler.text);
                txt = Data["body"];
            }
            GameObject gameObject = new GameObject("ChangelogText");
            gameObject.transform.SetParent(page.PageLayoutGroup.rectTransform);
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
            gameObject.transform.localScale = Vector3.one;
            TextMeshProUGUI textMeshProUGUI = gameObject.AddComponent<TextMeshProUGUI>();
            textMeshProUGUI.margin = new Vector4(25f, 0f, 50f, 0f);
            textMeshProUGUI.text = txt;
            page.ToggleScrollbar(true);
        }
        public static void create()
        {
            MelonCoroutines.Start(git());
        }
        public static void open(VRChatUtilityKit.Ui.TabButton tb)
        {
            page.gameObject.SetActive(true);

            tb.SubMenu.OpenSubMenu(page.uiPage);
        }
    }
}
