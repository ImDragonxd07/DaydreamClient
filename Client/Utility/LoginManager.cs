using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnhollowerBaseLib;
using UnityEngine;
using UnityEngine.UI;
using VRC.Core;
using static UnityEngine.UI.Button;

namespace Daydream.Client.Utility
{
    internal static class LoginManager
    {
        public static void login(string uname, string password)
        {
            Client.Utility.Logger.log("Waitfor login");
            if(Client.Classes.Daydream.isphotonbot == true)
            {
                MelonCoroutines.Start(wait(uname, password));

            }
        }

        private static IEnumerator wait(string uname, string password)
        {
            yield return new WaitUntil((Func<bool>)(() => GameObject.Find("UserInterface/MenuContent/Screens/Authentication/LoginUserPass/BoxLogin/InputFieldPassword").active == true));
            yield return new WaitForSeconds(5f);
            Utility.Logger.log("Login Screen");
            GameObject.Find("UserInterface/MenuContent/Screens/Authentication/StoreLoginPrompt/VRChatButtonLogin").GetComponent<Button>().onClick?.Invoke();
            VRCUiPageAuthentication[] authpages = Resources.FindObjectsOfTypeAll<VRCUiPageAuthentication>();
            VRCUiPageAuthentication loginPage = authpages.First((page) => page.gameObject.name == "LoginUserPass");
            Button loginButton = loginPage.transform.Find("ButtonDone (1)")?.GetComponent<Button>();
            GameObject.Find("UserInterface/MenuContent/Screens/Authentication/LoginUserPass/BoxLogin/InputFieldUser").GetComponent<UiInputField>().Method_Private_String_String_PDM_0(uname);
            yield return new WaitForSeconds(1f);
            GameObject.Find("UserInterface/MenuContent/Screens/Authentication/LoginUserPass/BoxLogin/InputFieldUser").GetComponent<Button>().onClick?.Invoke();
            yield return new WaitForSeconds(1f);
            GameObject.Find("UserInterface/MenuContent/Popups/InputPopup/ButtonRight").GetComponent<Button>().onClick?.Invoke();
            yield return new WaitForSeconds(1f);
            GameObject.Find("UserInterface/MenuContent/Screens/Authentication/LoginUserPass/BoxLogin/InputFieldPassword").GetComponent<UiInputField>().Method_Private_String_String_PDM_0(password);
            yield return new WaitForSeconds(1f);
            GameObject.Find("UserInterface/MenuContent/Screens/Authentication/LoginUserPass/BoxLogin/InputFieldPassword").GetComponent<Button>().onClick?.Invoke();
            yield return new WaitForSeconds(1f);
            GameObject.Find("UserInterface/MenuContent/Popups/InputPopup/ButtonRight").GetComponent<Button>().onClick?.Invoke();
            loginButton.onClick?.Invoke();
                Client.Utility.Logger.log("Waitfor login");
            yield return new WaitForEndOfFrame();
            while (GameObject.Find("UserInterface/MenuContent/Popups/InputCaptchaPopup/CaptchaImage").activeSelf == false) yield return null;
            while (GameObject.Find("UserInterface/MenuContent/Popups/InputCaptchaPopup/CaptchaImage").GetComponent<RawImage>().texture == null) yield return null;
            RawImage raw = GameObject.Find("UserInterface/MenuContent/Popups/InputCaptchaPopup/CaptchaImage").GetComponent<RawImage>();

            //Texture2D texture2D = ToTexture2D(raw.m_Texture);
            //texture2D = TextureHelper.ForceReadTexture(texture2D, default(Rect));
            //if (texture2D == null)
            //{
            //    Utility.Logger.errorlog("texture2D is null");
            //}

            //byte[] bytes = ImageConversion.EncodeToPNG(texture2D);
            //Utility.Logger.log("Send image to main ");
            //Utility.Logger.log("got bytes");
            //Client.Bots.Connection.ClientToMainCommand("captcha/" + Convert.ToBase64String(bytes));

            yield break;


        }

        public static Texture2D ToTexture2D(this Texture texture)
        {
            return Texture2D.CreateExternalTexture(
                texture.width,
                texture.height,
                TextureFormat.RGB24,
                false, false,
                texture.GetNativeTexturePtr());
        }
        private static void SetTextToUiInputField(UiInputField field, string text)
        {
            MethodInfo setTextMethod = typeof(UiInputField).GetMethod("set_text", BindingFlags.Public | BindingFlags.Instance);
            setTextMethod.Invoke(field, new object[] { text });
        }
        private static string GetTextFromUiInputField(UiInputField field)
        {
            /*
            FieldInfo textField = typeof(UiInputField).GetFields(BindingFlags.NonPublic | BindingFlags.Instance).First(f => f.FieldType == typeof(string) && f.Name != "placeholderInputText");
            return textField.GetValue(field) as string;
            */
            MethodInfo getTextMethod = typeof(UiInputField).GetMethod("get_text", BindingFlags.Public | BindingFlags.Instance);
            return getTextMethod.Invoke(field, new object[0]) as string;
        }
    }
}
