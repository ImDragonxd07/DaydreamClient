using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.Core;
using UnityEngine.XR;
using UnityEngine.UI;
using UnhollowerRuntimeLib.XrefScans;
using UnhollowerRuntimeLib;
using AvatarList = Il2CppSystem.Collections.Generic.List<VRC.Core.ApiAvatar>;
using UnityEngine.Networking;
using VRC.UI;
using VRC;
using MelonLoader.TinyJSON;
using Il2CppNewtonsoft.Json;

namespace Daydream.Client.Avatar
{
    internal class AvatarSearch : ReAvatar
    {
        private static HttpClient _httpClient;
        private static HttpClientHandler _httpClientHandler;
        private static AvatarList _searchedAvatars;

        public static void SetAvatarShown(ApiAvatar av)
        {
            try {
                GameObject.Find("UserInterface/MenuContent/Screens/Avatar").GetComponent<PageAvatar>().field_Public_SimpleAvatarPedestal_0.Method_Private_Void_ApiAvatar_0(av);

            }
            catch
            {
                Utility.Logger.errorlog("Couldent set avatar");
            }
        }
        public static void networkclient()
        {
            Utility.Logger.networklog("Connected to remod");
            _httpClientHandler = new System.Net.Http.HttpClientHandler

            {
                UseCookies = true,
                CookieContainer = new CookieContainer()
            };
            _searchedAvatars = new AvatarList();

            var vrHeadset = XRDevice.isPresent ? XRDevice.model : "Desktop";
            vrHeadset = vrHeadset.Replace(' ', '_');
            _httpClient = new HttpClient(_httpClientHandler);
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd($"{BuildInfo.Name}/{vrHeadset}.{BuildInfo.Version} ({SystemInfo.operatingSystem})");
            if(Utility.SaveData.getpin() == "none")
            {
                Utility.Logger.log("No pin");
                Ui.TabUi.QuickPopup("About", "Not logged in to Remod (Open menu and click remod)", "Continue");
            }
            else
            {
                Utility.Logger.log("Logging in");

                LoginToAPI(APIUser.CurrentUser, null);
            }
        }
        public static void loginpopup()
        {
            Client.Ui.TabUi.OpenPopup("Enter Pin", pin);

        }
        public static event Action<string> pin = delegate (string pinCode)
        {
            if (pinCode != "")
            {
                Utility.Logger.log("Set pin to " + pinCode);
                Utility.SaveData.setpin(pinCode);
                LoginToAPI(APIUser.CurrentUser, null);
            }
        };



        public static void SearchAvatar(string searchTerm)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://remod-ce.requi.dev/api/search.php?searchTerm={searchTerm}");
            Utility.Logger.log("Search for " + searchTerm);

            _httpClient.SendAsync(request).ContinueWith(rsp =>
            {
                Utility.Logger.log("Started search");

                var searchResponse = rsp.Result;
                if (!searchResponse.IsSuccessStatusCode)
                {
                    Utility.Logger.log("Search error");
                    if (searchResponse.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        Utility.Logger.errorlog($"Not logged into ReMod CE API anymore. Trying to login again and resuming request.");
                        LoginToAPI(APIUser.CurrentUser, () => SearchAvatar(searchTerm));
                        return;
                    }

                    searchResponse.Content.ReadAsStringAsync().ContinueWith(errorData =>
                    {

                        Utility.Logger.errorlog($"Could not search for avatars: \"{searchTerm}\"");
                        Utility.Logger.errorlog($"Could not search for avatars\nReason: \"{searchResponse.Content.ReadAsStringAsync().Result}\"");
                        Ui.TabUi.QuickPopup("Search Error", $"Fatal error: {searchResponse.Content.ReadAsStringAsync().Result}","Ok");
                    });
                }
                else
                {

                    Utility.Logger.log("Search found");
                    Utility.Logger.log($"Loading {searchResponse.Content.ReadAsStringAsync().Result.Count()} avatars ");

                    searchResponse.Content.ReadAsStringAsync().ContinueWith(t =>
                    {
                        List<ReAvatar> avatars = JSON.Load(t.Result.Replace("[","").Replace("]","")).Make<List<ReAvatar>>() ?? new List<ReAvatar>(); // ?? new List<ReAvatar>()
                        //var avatars = JsonConvert.DeserializeObject<List<ReAvatar>>(t.Result) ?? new List<ReAvatar>();

                        Utility.Logger.log("Done Converting Json");
                        
                        Utility.Logger.log($"Converted {avatars.Count} avatars");
                        Utility.Logger.log(avatars.ToString());

                        foreach (var item in avatars)
                        {
                            Utility.Logger.log("Avi" +  item.AvatarName.ToString());
                        }
                        MelonCoroutines.Start(RefreshSearchedAvatars(avatars));
                    });
                }
            });
        }
        private static int _loginRetries = 0;
        public static void changeavatar()
        {

        }
        private static IEnumerator RefreshSearchedAvatars(List<ReAvatar> results)
        {
           
            yield return new WaitForEndOfFrame();
            Utility.Logger.log("Started Loading Avatars ");
            _searchedAvatars.Clear();

            foreach (var avi in results.Select(x => x.AsApiAvatar()))
            {
                _searchedAvatars.Add(avi);
            }

            Utility.Logger.log($"Found {_searchedAvatars.Count} avatars");
            foundavatars = results.Count;
            //load avatars
            loadui();
        }
        public static string avataridt = "";
        public static string avataridt2 = "";
        public static string Image = "";

        public static int havicount = 0;
        public static GameObject curentgmj;

        private static GameObject createavi(GameObject gmj, string avatarid, string text, string image, string platform, int selectedid, string asseturl)
        {

            var ab = true;
            var inst = new GameObject();
            foreach (var a in Resources.FindObjectsOfTypeAll<VRCUiContentButton>())
            {
                if (a.name == "AvatarUiPrefab2" && ab)
                {
                    ab = false;
                    inst = GameObject.Instantiate(a, gmj.transform.Find("ViewPort/Content").transform).gameObject;

                }
            }
            var inst2 = new GameObject();
            inst.name = $"AvatarUiPrefab2_{text}";
            Component.DestroyImmediate(inst.GetComponent<VRCUiContentButton>());
            Component.DestroyImmediate(inst.transform.Find("RoomImageShape/RoomImage").gameObject.GetComponent<RawImage>());
            Component.DestroyImmediate(inst.GetComponent<UiFeatureList>());
            Component.DestroyImmediate(inst.GetComponent<UiFeaturedButton>());
            Component.DestroyImmediate(inst.transform.Find("RoomImageShape").gameObject.GetComponent<UnityEngine.UI.Mask>());
            Component.DestroyImmediate(inst.transform.Find("RoomImageShape").gameObject.GetComponent<UnityEngine.UI.Image>());
            inst.transform.Find("RoomImageShape").gameObject.SetActive(true);
            //  inst.gameObject.AddComponent<UnityEngine.UI.Button>();
            //inst.AddComponent<UnityEngine.UI.Image>();
            inst.transform.Find("TitleText").GetComponent<UnityEngine.UI.Text>().text = text;
            inst.transform.Find("TitleText").localPosition = new Vector3(-18.4f, -68.45f, -1);
            var img = inst.transform.Find("RoomImageShape/RoomImage").gameObject.AddComponent<UnityEngine.UI.Image>();
            var fav = inst.transform.Find("GrayScaleMask");
            fav.gameObject.SetActive(false);
            inst.transform.Find("RoomImageShape/RoomImage/Panel").gameObject.transform.localScale = new Vector3(1.03f, 1.1f, 1);
            inst.transform.Find("RoomImageShape/RoomImage/Panel").gameObject.transform.localPosition = new Vector3(0, -79f, 0);
            inst.transform.Find("RoomImageShape/RoomImage").gameObject.transform.localScale = new Vector3(0.95f, 0.92f, 0.9f);
            inst.transform.Find("RoomImageShape/RoomImage").gameObject.transform.localPosition = new Vector3(0, 0, -0.1f);

            fav.transform.localPosition = new Vector3(116.5892f, 70.813f, -0.3f);
            inst.gameObject.SetActive(true);
            MelonLoader.MelonCoroutines.Start(Client.Assets.Resources.LoadImageFromUrl(img, image,false));
            var gmjs = new GameObject();
            gmjs.transform.parent = inst.transform;
            gmjs.transform.localPosition = new Vector3(0, 0, 0);
            gmjs.transform.localRotation = new Quaternion(0, 0, 0, 0);
            gmjs.transform.localScale = new Vector3(1, 1, 1);
            gmjs.transform.localRotation = new Quaternion(0, 0, 0, 0);
            gmjs.gameObject.AddComponent<UnityEngine.UI.Button>();
            gmjs.gameObject.AddComponent<UIInvisibleGraphic>();
            // gmjs.transform.localScale = new Vector3(3, 0.2f, 1);
            gmjs.transform.localScale = new Vector3(3, 1.8f, 1);

            inst.transform.Find("TitleText").gameObject.SetActive(true);
            inst.transform.Find("RoomOutline").gameObject.SetActive(true);
            var lay = inst.GetComponent<UnityEngine.UI.LayoutElement>();
            lay.ignoreLayout = false;
            inst.transform.Find("RoomImageShape/OverlayIcons/MobileIcons").transform.localPosition = new Vector3(0f, -32.84f, 0);
            if (platform == "StandaloneWindows")
                inst.transform.Find("RoomImageShape/OverlayIcons/MobileIcons/IconPlatformPC").gameObject.SetActive(true);
            else if (platform == "All")
                inst.transform.Find("RoomImageShape/OverlayIcons/MobileIcons/IconPlatformAny").gameObject.SetActive(true);
            else
                inst.transform.Find("RoomImageShape/OverlayIcons/MobileIcons/IconPlatformMobile").gameObject.SetActive(true);
            gmjs.transform.localPosition = new Vector3(0, 0, 0);
            gmjs.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(new Action(() => {

                if (selectedid == 1)
                    avataridt = avatarid;
                else
                {
                    Image = image;
                    avataridt2 = avatarid;
                    curentgmj = inst;
                }
                Utility.Logger.log("Changing into " + avatarid);

                Avatar.AvatarSearch.SetAvatarShown(new ApiAvatar { id = avatarid, assetUrl = asseturl } );
                  foreach (var a in gmj.transform.Find("ViewPort/Content").gameObject.GetComponentsInChildren<UnityEngine.UI.LayoutElement>(true))
                {
                    if (a.name.Contains("AvatarUiPrefab2"))
                    {
                        a.transform.Find("RoomImageShape/OverlayIcons/iconFavoriteStar").gameObject.SetActive(false);
                    }
                }
                inst.transform.Find("RoomImageShape/OverlayIcons/iconFavoriteStar").gameObject.SetActive(true);

            }));

            return inst;
        }

        public static int foundavatars = 0;

        public static int recievedavatars = 0;
        public static int avatarlimit = 100;
        private static IEnumerator UpdateScrollRect(ScrollRectEx ScrolRect)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            ScrolRect.movementType = ScrollRect.MovementType.Elastic;
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            ScrolRect.movementType = ScrollRect.MovementType.Clamped;
            ScrolRect.m_Inertia = false;
        }
        public static void loadui()
        {
            Utility.Logger.log("Update UI");
            recievedavatars = 0;
            UiAvatarList list = Client.Ui.Ui.AvatarContainer.GetComponent<UiAvatarList>();
            ScrollRectEx scroll = Client.Ui.Ui.AvatarContainer.GetComponent<ScrollRectEx>();
            foreach (var rect in scroll.content)
            {
                GameObject.Destroy(rect.TryCast<RectTransform>().gameObject);
            }
            //list.clearUnseenListOnCollapse = false;
            //list.field_Private_Dictionary_2_String_ApiAvatar_0.Clear();
            //list.field_Public_Category_0 = UiAvatarList.Category.SpecificList;
            foreach (var objet in _searchedAvatars)
            {
                if(recievedavatars < avatarlimit)
                {
                    createavi(Client.Ui.Ui.AvatarContainer, objet.id, objet.name, objet.thumbnailImageUrl, objet.platform, 0, objet.assetUrl);
                }
                recievedavatars++;
            }
            Text title = Client.Ui.Ui.AvatarContainer.transform.Find("Button/TitleText").GetComponent<Text>();
            title.text = "Daydream (loaded " + recievedavatars + " / " + _searchedAvatars.Count + ")";
            MelonCoroutines.Start(UpdateScrollRect(Daydream.Client.Ui.Ui.AvatarContainer.GetComponent<ScrollRectEx>()));
            //Daydream.Client.Ui.Ui.AvatarContainer.GetComponent<ScrollRectEx>().inertia = false;
            //Daydream.Client.Ui.Ui.AvatarContainer.GetComponent<ScrollRectEx>().movementType = ScrollRect.MovementType.Unrestricted;
        }
        private static GameObject avilist(string name)
        {
            var gmj = GameObject.Find("/UserInterface").transform.Find("MenuContent/Screens/Avatar/Vertical Scroll View/Viewport/Content/Favorite Avatar List").gameObject;
            var instanciated = GameObject.Instantiate(gmj, gmj.transform.parent);
            Component.Destroy(instanciated.GetComponent<UiAvatarList>());
            instanciated.name = $"Favp_{name}";
            var avif = instanciated.transform.Find("ViewPort/Content");

            foreach (var a in avif.GetComponentsInChildren<UnityEngine.UI.LayoutElement>())
            {
                GameObject.Destroy(a.gameObject);
            }

            try
            {
                GameObject.Destroy(instanciated.transform.Find("GetMoreFavorites").gameObject);

            }
            catch { Utility.Logger.errorlog("Failed TO destroy FavortiesButton"); }
            instanciated.transform.Find("Button/TitleText").gameObject.GetComponent<UnityEngine.UI.Text>().text = name;
            instanciated.gameObject.SetActive(true);
            instanciated.transform.SetSiblingIndex(0);
            try
            {
                GameObject.Destroy(instanciated.transform.Find("Button/ToggleIcon").gameObject);

            }
            catch { Utility.Logger.errorlog("Failed TO destroy Button/ToggleIcon"); }
            instanciated.transform.Find("Button/TitleText").transform.localPosition = new Vector3(63.7724f, 0, 0);


            return instanciated;
        }

        private static void SendAvatarRequest(HttpMethod method, Action<HttpResponseMessage> onResponse)
        {

            var request = new HttpRequestMessage(method, $"https://remod-ce.requi.dev/api/avatar.php");

            _httpClient.SendAsync(request).ContinueWith(t => onResponse(t.Result));
        }

        private static void LoginToAPI(APIUser currentUser, Action onLogin)
        {
            if (_loginRetries >= 3)
            {
                Utility.Logger.errorlog($"Could not login to ReMod API: Exceeded retries. Please restart your game and make sure your pin is correct!");
                return;
            }
            Utility.Logger.log("Attempting to log into remod from daydream " + "(" + APIUser.CurrentUser.id + ")");

            var request = new HttpRequestMessage(HttpMethod.Post, $"https://remod-ce.requi.dev/api/login.php")
            {
                Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                {

                    new("user_id", APIUser.CurrentUser.id),
                    new("pin", Utility.SaveData.getpin())
                })
            };
            Utility.Logger.log("Connecting to remod server");

            ++_loginRetries;

            _httpClient.SendAsync(request).ContinueWith(t =>
            {
                Utility.Logger.log("Sending request to remod server");

                var loginResponse = t.Result;
                if (!loginResponse.IsSuccessStatusCode)
                {
                    loginResponse.Content.ReadAsStringAsync().ContinueWith(tsk =>
                    {
                        Utility.Logger.errorlog($"Content: " + t.Result.Content.ReadAsStringAsync().Result);

                        Utility.Logger.errorlog($"Could not login to ReMod API: " + t.Result);
                        Ui.TabUi.QuickPopup("Error", "Could not log in to remod (Check console for details)", "Continue");

                    });
                }
                else
                {
                    Utility.Logger.networklog($"Logged in to REMOD");


                    _loginRetries = 0;

                    //onLogin();
                }
            });
        }

     }
}

