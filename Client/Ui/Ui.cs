using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VRC.Core;
using VRC.UI;
using VRC.Utility;

namespace Daydream.Client.Ui
{
    internal class Ui
    {
        private static GameObject CanvasObject;
        private static CanvasScaler scaler;
        private static GameObject BackgroundObject;
        private static Canvas canvas;
        private static RectTransform rect;
        private static CanvasRenderer render;
        private static Image frameimage;
        private static GameObject clientname;
        private static Text clienttext;
        private static CanvasRenderer clientuirenderer;
        private static GameObject worldname;
        private static Text worldtext;
        private static CanvasRenderer worlduirender;
        public static void build()
        {
            Utility.Logger.log("Build Ui");
            //VRChatUtilityKit.Ui.UiManager.OnBigMenuClosed += Client.Ui.TabUi.BigMenuClosed;
            Ui.CanvasObject = new GameObject("DaydreamUI");
            UnityEngine.Object.DontDestroyOnLoad(Ui.CanvasObject);
            Ui.CanvasObject.transform.position = Vector3.zero;
            Ui.canvas = Ui.CanvasObject.AddComponent<Canvas>();
            Ui.canvas.renderMode = RenderMode.ScreenSpaceCamera;
            Ui.canvas.worldCamera = Camera.main;
            Ui.scaler = Ui.CanvasObject.AddComponent<CanvasScaler>();
            Ui.scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            Ui.scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            Ui.scaler.referencePixelsPerUnit = 100;
            Ui.CanvasObject.SetActive(true);
            Utility.Logger.log("Ui Built");
        }
        private static bool exists = false;
        private static bool created = false;
        private static bool isplaying = false;
        private static GameObject notipannel;
        private static GameObject notiftext;
        private static Vector3 open;
        private static Vector3 close;
        public static void Notification(string txt)
        {
            MelonCoroutines.Start(ShowNotification(txt));
        }
        public static IEnumerator ShowNotification(string text)
        {
            yield return new WaitUntil((Func<bool>)(() => Assets.Resources.loadedassets == true));

            if (created == false)
            {
                Ui.notipannel = Assets.Resources.notificationui;
                Ui.notipannel.transform.SetParent(GameObject.Find("/UserInterface/PlayerDisplay/WorldHudDisplay").transform);
                created = true;
            }
            //Ui.notiftext.GetComponent<Text>().text = text;
            // Ui.notipannel.gameObject.SetActive(true);
            Ui.notipannel.gameObject.SetActive(false);

        }
        private UnityAction<string> _searchAvatarsAction;


        public static GameObject avatarbutton;
        private static UnityAction<string> _overrideSearchAvatarsAction;

        public static void updateavatar()
        {
            GameObject input = GameObject.Find("UserInterface/MenuContent/Backdrop/Header/Tabs/ViewPort/Content/Search/InputField");
            if(input == null)
            {
                return;
            }
            if(GameObject.Find("UserInterface/MenuContent/Screens/Avatar").active == true && !input.GetComponent<Button>().interactable)
            {

                input.GetComponent<Button>().interactable = true;
                
                input.GetComponent<UiInputField>().field_Public_UnityAction_1_String_0 = _overrideSearchAvatarsAction;
            }

        }
        public static GameObject AvatarContainer;
        private static void customopenfix()
        {

            //GameObject.Find("UserInterface/MenuContent/Backdrop/Header/Tabs/ViewPort/Content/AvatarPageTab/Button").GetComponent<Button>().onClick?.Invoke();
            //GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)").active = false;

        }
        private static IEnumerator startavatar()
        {


            yield return new WaitUntil((Func<bool>)(() => GameObject.Find("UserInterface/MenuContent/Screens/Avatar/Vertical Scroll View/Viewport/Content/Public Avatar List") != null));
            yield return new WaitUntil((Func<bool>)(() => GameObject.Find("UserInterface/MenuContent/Backdrop/Header/Tabs/ViewPort/Content/Search/InputField") != null));
            yield return new WaitUntil((Func<bool>)(() => GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Avatars") != null));

            Utility.Logger.log("Done waiting");
            //GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Avatars").GetComponent<Button>().onClick.RemoveAllListeners();
            //GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Avatars").GetComponent<Button>().onClick.AddListener(new System.Action(() => { customopenfix(); })); ;
            _overrideSearchAvatarsAction = DelegateSupport.ConvertDelegate<UnityAction<string>>((Action<string>)Client.Avatar.AvatarSearch.SearchAvatar);

            AvatarContainer = GameObject.Instantiate(GameObject.Find("UserInterface/MenuContent/Screens/Avatar/Vertical Scroll View/Viewport/Content/Public Avatar List"), GameObject.Find("UserInterface/MenuContent/Screens/Avatar/Vertical Scroll View/Viewport/Content").transform,true);
            AvatarContainer.name = "Daydream";
            yield return new WaitUntil((Func<bool>)(() => AvatarContainer.GetComponent<ScrollRectEx>().content.childCount > 0));
  

            UiInputField search = GameObject.Find("UserInterface/MenuContent/Backdrop/Header/Tabs/ViewPort/Content/Search/InputField").GetComponent<UiInputField>();
            AvatarContainer.transform.SetAsFirstSibling();

            Utility.Logger.log("search override");
            
            AvatarContainer.SetActive(true);
            Text title = AvatarContainer.transform.Find("Button/TitleText").GetComponent<Text>();
            title.text = "Daydream";
            GameObject.Find("UserInterface/MenuContent/Screens/Avatar/Change Button").GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject.Find("UserInterface/MenuContent/Screens/Avatar/Change Button").GetComponent<Button>().onClick.AddListener(new System.Action(() => {
                Utility.Extentions.ChangeToAvatar(VRCPlayer.field_Internal_Static_VRCPlayer_0, GameObject.Find("UserInterface/MenuContent/Screens/Avatar").GetComponent<PageAvatar>().field_Public_SimpleAvatarPedestal_0.field_Internal_ApiAvatar_0.id);
            }));
            //avatarbutton = GameObject.Instantiate(AvatarContainer.GetComponent<ScrollRectEx>().content.GetChild(1).gameObject, AvatarContainer.transform.Find("ViewPort/Content"));
            //avatarbutton.gameObject.name = "AvatarTemplate";
            //GameObject.DontDestroyOnLoad(avatarbutton);
        }
        public static void AvatarSearchUI()
        {

            MelonCoroutines.Start(startavatar());
            //GameObject.Find("UserInterface/MenuContent/Screens/Avatar").GetComponent<PageAvatar>().field_Public_SimpleAvatarPedestal_0.Method_Public_Void_ApiAvatar_PDM_0(new ApiAvatar { id = VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_ApiAvatar_0.id, assetUrl = VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_ApiAvatar_0.assetUrl })
        }
        public static void tabui(string worldname)
        {
            if (exists == false)
            {
                exists = true;
                Utility.Logger.log("Create World Panel");
                Ui.BackgroundObject = new GameObject("Panel");
                Utility.Logger.log("Create Rect");
                Ui.rect = Ui.BackgroundObject.AddComponent<RectTransform>();
                Ui.rect.anchorMin = new Vector2(0f, 1f);
                Ui.rect.anchorMax = new Vector2(0f, 1f);
                Ui.rect.pivot = new Vector2(0.5f, 0.5f);
                Ui.rect.sizeDelta = new Vector2(166f, 55f);
                Ui.rect.localPosition += new Vector3(83f, -27f, 0f);
                Utility.Logger.log("Create Render");
                Ui.render = Ui.BackgroundObject.AddComponent<CanvasRenderer>();
                Ui.frameimage = Ui.BackgroundObject.AddComponent<Image>();
                Ui.frameimage.color = new Color32(58, 58, 58, 0);
                Utility.Logger.log("Set Parent");
                Utility.Logger.log("Set Clientname");
                Ui.clientname = new GameObject("Clientname");
                Ui.clienttext = Ui.clientname.AddComponent<Text>();

                Ui.clienttext.text = $"Daydream (v.{Classes.Daydream.version})";
                Ui.clienttext.color = Color.cyan;
                Ui.clienttext.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                Ui.clienttext.fontSize = 10;
                Ui.clienttext.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                Ui.clienttext.GetComponent<RectTransform>().sizeDelta = new Vector2(165f, 17f);
                Ui.clienttext.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 1f);
                Ui.clienttext.GetComponent<RectTransform>().anchorMax = new Vector2(0f, 1f);
                Ui.clienttext.GetComponent<RectTransform>().localPosition += new Vector3(83f, -9f, 0f);
                Ui.clientuirenderer = Ui.clientname.AddComponent<CanvasRenderer>();

                Utility.Logger.log("Set Worldname");
                //Ui.worldname = new GameObject("Worldname");
                //Ui.worldtext = Ui.worldname.AddComponent<Text>();
                //Ui.worldtext.color = Color.white;
                //Ui.worldtext.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                //Ui.worldtext.fontSize = 10;
                //Ui.worldtext.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                //Ui.worldtext.GetComponent<RectTransform>().sizeDelta = new Vector2(165f, 17f);
                //Ui.worldtext.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 1f);
                //Ui.worldtext.GetComponent<RectTransform>().anchorMax = new Vector2(0f, 1f);
                //Ui.worldtext.GetComponent<RectTransform>().localPosition += new Vector3(83f, -24f, 0f);
                //Ui.worlduirender = Ui.worldname.AddComponent<CanvasRenderer>();
                Utility.Logger.log("Created Ui");
                Ui.BackgroundObject.transform.SetParent(Ui.CanvasObject.transform, false);
                Ui.clientname.transform.SetParent(Ui.BackgroundObject.transform, false);
                //Ui.worldname.transform.SetParent(Ui.BackgroundObject.transform, false);
            }
            else
            {
                //Ui.worldtext.text = worldname;
            }
        }
        private static GameObject hackframe;
        private static RectTransform hacktransform;
        private static Image hackimage;
        private static CanvasRenderer hackrender;
        private static VerticalLayoutGroup list;
        private static GameObject hacktextframe;
        private static Text Hacktext;
        private static RectTransform Hacktextrect;
        private static Renderer textrender;
        //public static void selectinmenuUI()
        //{
        //    MelonCoroutines.Start(waitforsocial());
        //}
        //private static IEnumerator waitforsocial()
        //{
        //    yield return new WaitUntil((Func<bool>)(() => GameObject.Find("UserInterface/MenuContent/Screens/UserInfo/Buttons/RightSideButtons/RightUpperButtonColumn/EditStatusButton") != null));
        //    GameObject newbutton = GameObject.Instantiate(GameObject.Find("UserInterface/MenuContent/Screens/UserInfo/Buttons/RightSideButtons/RightUpperButtonColumn/EditStatusButton"));
        //    newbutton.transform.SetParent(GameObject.Find("UserInterface/MenuContent/Screens/UserInfo/Buttons/RightSideButtons/RightUpperButtonColumn").transform);
        //    newbutton.transform.localPosition = new Vector3(30, -290, 0);
        //    newbutton.transform.localScale = new Vector3(1, 1, 1);
        //    newbutton.transform.localEulerAngles = GameObject.Find("UserInterface/MenuContent/Screens/UserInfo/Buttons/RightSideButtons/RightUpperButtonColumn/EditStatusButton").transform.localEulerAngles;
        //    //newbutton.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Select";
        //    newbutton.GetComponent<Button>().onClick.RemoveAllListeners();
        //    newbutton.GetComponent<Button>().onClick.AddListener(new System.Action(() => { click(); }));
        //    newbutton.transform.GetComponentInChildren<Text>().text = "Select";
        //}
        //private static System.Action click()
        //{
        //    Utility.Logger.log("click");
        //    VRChatUtilityKit.Ui.UiManager.CloseBigMenu();
        //    VRChatUtilityKit.Ui.UiManager.OpenQuickMenu();
        //    VRChatUtilityKit.Ui.UiManager.OpenUserInQuickMenu(VRChatUtilityKit.Utilities.VRCUtils.ActiveUserInUserInfoMenu);
        //    return null;
        //}
        public static void HackUi(bool install, string hack, bool add)
        {
            if (install == true)
            {
                Ui.hackframe = new GameObject("HackFrame");
                Ui.hacktransform = Ui.hackframe.AddComponent<RectTransform>();
                Ui.hacktransform.anchorMin = new Vector2(0f, 1f);
                Ui.hacktransform.anchorMax = new Vector2(0f, 1f);
                Ui.hacktransform.pivot = new Vector2(0.5f, 0.5f);
                Ui.hacktransform.sizeDelta = new Vector2(74f, 137f);
                Ui.hacktransform.localPosition += new Vector3(37f, -135f, 0f);
                Ui.hackrender = Ui.hackframe.AddComponent<CanvasRenderer>();
                Ui.hackimage = Ui.hackframe.AddComponent<Image>();
                Ui.hackimage.color = new Color32(58, 58, 58, 0);
                Ui.hackframe.transform.SetParent(Ui.CanvasObject.transform, false);
                Ui.list = Ui.hackframe.AddComponent<VerticalLayoutGroup>();
                Ui.list.childForceExpandHeight = false;

            }
            else
            {
                if (add == true)
                {
                    Utility.Logger.log("Adding " + hack);
                    if (!GameObject.Find(hack))
                    {
                        Ui.hacktextframe = new GameObject(hack);
                        Ui.hacktextframe.transform.SetParent(Ui.hackframe.transform, false);
                        Ui.Hacktextrect = Ui.hacktextframe.AddComponent<RectTransform>();
                        Ui.Hacktext = Ui.hacktextframe.AddComponent<Text>();
                        Ui.Hacktext.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                        Ui.Hacktext.fontSize = 10;
                        Ui.Hacktext.text = hack;
                    }
                    //ADD ITEM
                }
                else
                {
                    if (GameObject.Find(hack))
                    {
                        GameObject.Destroy(GameObject.Find(hack));
                    }
                }
            }
        }
        public static void vrmenu()
        {

        }

    }
}
