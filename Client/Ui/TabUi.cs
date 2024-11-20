using MelonLoader;
using Daydream.Client.Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VRC;
using VRC.DataModel;
using VRC.SDKBase;
using VRC.Core;
using VRC.Utility;
using UnhollowerRuntimeLib;
using VRC.UI;
using VRC.UI.Elements.Menus;
using VRC.DataModel.Core;
using VRChatUtilityKit.Ui;
using VRChatUtilityKit.Utilities;

namespace Daydream.Client.Ui
{
    internal class TabUi
    {
        private static VRC.UI.Elements.QuickMenu _quickMenuInstance;



        public static VRCPlayerApi getselectedplayer()
        {
            return Utility.Extentions.GetPlayerApi(VRChatUtilityKit.Utilities.VRCUtils.ActivePlayerInUserSelectMenu);
        }
        public static ApiAvatar getselectedavatar()
        {
            return VRChatUtilityKit.Utilities.VRCUtils.ActivePlayerInUserSelectMenu.prop_ApiAvatar_0;
        }
        public static IUser getselectediuser()
        {
            return VRChatUtilityKit.Utilities.VRCUtils.ActiveUserInUserSelectMenu;
        }
        private static IEnumerator WaitForUiManagerInit()
        {
            yield return new WaitUntil((Func<bool>)(() => Classes.Daydream.firstjoin == true));
            yield break;
        }

        public static void BigMenuClosed()
        {
            Utility.Logger.log("Big menu closed");
            try
            {
                
                //GameObject.Find("UserInterface/MenuContent/Screens/Avatar").GetComponent<PageAvatar>().field_Public_SimpleAvatarPedestal_0.Method_Public_Void_ApiAvatar_PDM_0(VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_ApiAvatar_0);
                Avatar.AvatarSearch.SetAvatarShown(VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_ApiAvatar_0);

                
            }
            catch
            {

            }


        }

        public static void OpenPopup(string title, Action<string> popupfinished)

        {
            Utility.Logger.log("popup");
            VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Public_Void_String_String_InputType_Boolean_String_Action_3_String_List_1_KeyCode_Text_Action_String_Boolean_Action_1_VRCUiPopup_Boolean_PDM_0(title, "", InputField.InputType.Standard, false, "Enter",
                DelegateSupport.ConvertDelegate<Il2CppSystem.Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text>>
                (new Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text> 
                (delegate (string s, Il2CppSystem.Collections.Generic.List<KeyCode> k, Text t)
                {
                    Utility.Logger.log("popup code: " + s);
                    popupfinished(s);
                })), null, "...", true, null);;;
        }
        public static void OnApplicationStart()
        {

            MelonCoroutines.Start(WaitForUiManagerInit());
           // VRChatUtilityKit.Utilities.VRCUtils.OnUiManagerInit += VRChat_OnUiManagerInit;
           // VRChatUtilityKit.Utilities.VRCUtils.OnUiManagerInit += PlayerMenu;

        }

        //(Sprite)MelonCoroutines.Start(Assets.Resources.Loadimage("C:\\Program Files (x86)\\Steam\\steamapps\\common\\VRChat\\DaydreamAssets\\Resources\\tab.PNG"))
        private static IEnumerator waitforgameobject(GameObject go)
        {
            yield return new WaitUntil((Func<bool>)(() => go != null));
        }
        //public static VRChatUtilityKit.Ui.SubMenu createuipage(string name)
        //{
        //    VRChatUtilityKit.Ui.SubMenu sm = new VRChatUtilityKit.Ui.SubMenu(name, "Submenu_" + name, name);
        //    MelonCoroutines.Start(waitforgameobject(sm.gameObject));
        //    return sm;
        //}

        public static void QuickPopup(string Title, string Text, String Buttontext)
        {
            //var popup = typeof(VRCUiPopupManager).GetMethods().First(mb => mb.Name.StartsWith("Method_Public_Void_String_String_String_Action_Action_1_VRCUiPopup_") && !mb.Name.Contains("PDM") && XrefUtils.CheckUsedBy(mb, "OpenSaveSearchPopup"));
            //popup.Invoke(VRCUiPopupManager.prop_VRCUiPopupManager_0, new object[4] { Title, Text, Buttontext, (Il2CppSystem.Action)new Action(() => { VRCUiManager.prop_VRCUiManager_0.HideScreen("POPUP"); }) });
            VRChatUtilityKit.Ui.UiManager.OpenSmallPopup(Title,Text,Buttontext, new Action(() => { VRChatUtilityKit.Ui.UiManager.ClosePopup(); }));
        }
        public static TabButton myTabButton;
        private static IEnumerator seticon()
        {
            yield return new WaitUntil((Func<bool>)(() => TabUi.myTabButton.gameObject.active));
            Image img = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/DaydreamTab/Icon").GetComponent<Image>();
            MelonCoroutines.Start(Daydream.Client.Assets.Resources.LoadImageFromUrl(img, URLS.TabIcon, true));
            yield break;
        }
        public static SubMenu createuipage(string name)
        {
            SubMenu subMenu = new SubMenu(name, "Submenu_" + name, name, null);
            MelonCoroutines.Start(TabUi.waitforgameobject(subMenu.gameObject));
            return subMenu;
        }
        private static IEnumerator startpatch()
        {
            yield return new WaitUntil((Func<bool>)(() => GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Daydream") != null));

            GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Daydream").SetActive(true);

        }
        private static void patchedopen()
        {
            GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Daydream").SetActive(true);
            VRChatUtilityKit.Ui.UiManager.OpenQuickMenuPage("Daydream");
        }
        public static void VRChat_OnUiManagerInit()
        {
            MelonCoroutines.Start(startpatch()); // FIXES THE PROBLEM WITH VRCUTILITYKIT
            TabUi.myTabButton = new TabButton(null, "Daydream", "DaydreamTab", "Daydream", "Daydream", null);
            MelonCoroutines.Start(TabUi.seticon());
            GameObject gameObject = new GameObject();
            UnityEngine.Object.Instantiate<GameObject>(gameObject);
            UnityEngine.Object.DontDestroyOnLoad(gameObject);
            Daydream.Client.Utility.Logger.log("Loading tab UI");
            myTabButton.gameObject.SetActive(true);

            Pages.Functions.create();
            //Pages.Fun.create();
            Pages.AppBotFunctions.create();
            Pages.Changelog.create();
            Pages.Laggers.create();
            Pages.Protections.create();
            Pages.ReMod.create();
            Pages.Settings.create();
            Pages.Utility.create();
            Pages.Bots.create();
            Pages.Udon.create();
            myTabButton.SubMenu.AddButtonGroup(new VRChatUtilityKit.Ui.ButtonGroup("Daydream VRC", "Daydream VRC", new System.Collections.Generic.List<VRChatUtilityKit.Ui.IButtonGroupElement>()
              {
               new VRChatUtilityKit.Ui.SingleButton(() => Pages.Functions.open(myTabButton), null, "Functions", "Functions"),
               //new VRChatUtilityKit.Ui.SingleButton(() => Pages.Fun.open(myTabButton), null, "Fun", "Fun"),
               new VRChatUtilityKit.Ui.SingleButton(() => Pages.Laggers.open(myTabButton), null, "Laggers", "laggers"),
               new VRChatUtilityKit.Ui.SingleButton(() => Pages.Protections.open(myTabButton), null, "Protections", "Protections"),
               new VRChatUtilityKit.Ui.SingleButton(() => Pages.Utility.open(myTabButton), null, "Utility", "Utility"),
               new VRChatUtilityKit.Ui.SingleButton(() => Pages.Bots.open(myTabButton), null, "Bots", "Bots"),
               new VRChatUtilityKit.Ui.SingleButton(() => Pages.Udon.open(myTabButton), null, "Udon", "Udon"),
              }));
            myTabButton.SubMenu.AddButtonGroup(new VRChatUtilityKit.Ui.ButtonGroup("Other", "Other", new System.Collections.Generic.List<VRChatUtilityKit.Ui.IButtonGroupElement>()
              {
               new VRChatUtilityKit.Ui.SingleButton(() => Pages.Settings.open(myTabButton), null, "Settings", "Settings"),
               new VRChatUtilityKit.Ui.SingleButton(() => Pages.Changelog.open(myTabButton), null, "Changelog", "Changelog"),
               new VRChatUtilityKit.Ui.SingleButton(() => Pages.ReMod.open(myTabButton), null, "ReMod", "ReMod"),

              }));
            myTabButton.SubMenu.AddButtonGroup(new VRChatUtilityKit.Ui.ButtonGroup("Community", "Community", new System.Collections.Generic.List<VRChatUtilityKit.Ui.IButtonGroupElement>()
              {               
                new VRChatUtilityKit.Ui.SingleButton(() => Application.OpenURL($"{Classes.Daydream.BaseWebsiteurl}"), null, "Website", "Website", "Opens the Daydream website"),
                new VRChatUtilityKit.Ui.SingleButton(() => Application.OpenURL($"{Classes.Daydream.BaseWebsiteurl}/Discord.html"), null, "Discord", "Discord", "Opens the Daydream Discord"),


              }));
            myTabButton.SubMenu.ToggleScrollbar(true);
            myTabButton.ButtonComponent.onClick.RemoveAllListeners();
            myTabButton.ButtonComponent.onClick.AddListener(new System.Action(() => { patchedopen(); }));
            Utility.Logger.log("Loaded tab UI");

        }
        public static void PlayerMenu()
        {
            Utility.Logger.log("Loading Player Menu");
            ButtonGroup User = new VRChatUtilityKit.Ui.ButtonGroup("Daydream User Functions", "Daydream User Functions", new System.Collections.Generic.List<VRChatUtilityKit.Ui.IButtonGroupElement>()
            {
                 new VRChatUtilityKit.Ui.SingleButton(() => Hacks.tpto.tptoselected(), null, "Teleport", "Teleport", "Teleports you to the selected player"),
                 new VRChatUtilityKit.Ui.SingleButton(() => Hacks.RipAvatar.DownloadSelect(), null, "Rip Avatar", "Rip Avatar", "Downloads the selected players avatar"),
                 new VRChatUtilityKit.Ui.SingleButton(() => Hacks.ForceClone.allowclone(), null, "Force Clone", "Force Clone", "Allows you to clone the selected avatar"),
                 new VRChatUtilityKit.Ui.SingleButton(() => Utility.PortalManager.dropportalonselected(), null, "Force TP", "Force TP", "Teleports the selected player to another world with portals"),
                 new VRChatUtilityKit.Ui.SingleButton(() => Hacks.CopyAvId.copy(), null, "Copy Av Id", "Copy Id", "Copys selected players avatar id to clipboard"),
                 new VRChatUtilityKit.Ui.SingleButton(() => Hacks.SoftClone.SoftCloneSelected(), null, "Soft clone", "softclone", "Local clones any avatar"),

            });
            Utility.Logger.log("Set user option parent");
            User.Header.gameObject.transform.SetParent(((Component)GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup")).transform);
            User.gameObject.transform.SetParent(((Component)GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup")).transform);

            User.Header.gameObject.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, 0);
            User.gameObject.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, 0);
            User.Header.gameObject.GetComponent<RectTransform>().localRotation = new Quaternion(0, 0, 0, 0);
            User.gameObject.GetComponent<RectTransform>().localRotation = new Quaternion(0, 0, 0, 0);
            User.Header.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(User.Header.gameObject.GetComponent<RectTransform>().localPosition.x, User.Header.gameObject.GetComponent<RectTransform>().localPosition.y, 0);
            User.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(User.ButtonLayoutGroup.gameObject.GetComponent<RectTransform>().localPosition.x, User.ButtonLayoutGroup.gameObject.GetComponent<RectTransform>().localPosition.y, 0);
            User.Header.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);

            User.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);


            User.gameObject.SetActive(true);
            Utility.Logger.log("Loaded Player Menu");
        }
    }
}
