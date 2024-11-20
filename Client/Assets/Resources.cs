using Il2CppSystem.Collections.Generic;
using MelonLoader;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Reflection;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Daydream.Client.Assets
{
    public static class URLS {
        public static string TabIcon = "https://drive.google.com/uc?export=download&id=1hTLM6sVP0OULbLHtDEeeRHz_URzjUQ2F";
    }

    internal static class Resources
    {
        private static AssetBundle _bundle;
        public static Sprite Plaunch, joinnoti;
        private static AssetBundle Bundle;
        public static GameObject notificationui;
        public static Sprite clientlogo, DiscordLogo, GitHubLogo,
            Tab, keyboard, mapmarker, one, two, three, four, ver;
        public static Texture2D back,down,front,left,right,up;
        public static Material skybox;
        public static bool loadedassets = false;
        public static IEnumerator LoadImageFromUrl(Image Instance, string url, bool makeuicolor)
        {

            var www = UnityWebRequestTexture.GetTexture(url);
            _ = www.downloadHandler;
            var asyncOperation = www.SendWebRequest();
            Func<bool> func = () => asyncOperation.isDone;
            yield return new WaitUntil(func);
            if (www.isHttpError || www.isNetworkError)
            {
                Debug.Log("Error2 : " + www.error);
                yield break;
            }

            var content = DownloadHandlerTexture.GetContent(www);
            Texture2D showTexture;
            if (makeuicolor == true)
            {
                Color[] mapPixels = content.GetPixels(0);
                showTexture = GameObject.Instantiate(content) as Texture2D;

                for (int i = 0, j = showTexture.width; i < j; i += 1)
                {
                    for (int k = 0, l = showTexture.height; k < l; k += 1)
                    {
                        if(content.GetPixel(i, k).a > 0f)
                        {
                            showTexture.SetPixel(i, k, new Color(0.4157f, 0.8902f, 0.9765f, 1f));

                        }

                    }
                }

                showTexture.Apply(true);
            }
            else
            {
                showTexture = content;
            }
            var sprite2 = Instance.overrideSprite = Sprite.CreateSprite(showTexture,
                new Rect(0f, 0f, content.width, content.height), new Vector2(0f, 0f), 100000f, 1000u,
                SpriteMeshType.FullRect, Vector4.zero, false);
            Instance.color = Color.white;
            if (sprite2 != null) Instance.overrideSprite = sprite2;
        }
        private static bool courtinedone = false;
        public static Texture2D gettexturefromurldirect(string url)
        {
            Texture2D img = new Texture2D(128,128);
            MelonCoroutines.Start(LoadTextureFromUrl(img, url));

            while (courtinedone == true)
            {

            }
            return img;
        }
        public static IEnumerator LoadSprieFromUrl(Sprite sprite,string url)
        {
            Utility.Logger.log("Downloading " + url);

            var www = UnityWebRequestTexture.GetTexture(url);
            _ = www.downloadHandler;
            var asyncOperation = www.SendWebRequest();

            Func<bool> func = () => asyncOperation.isDone;
            yield return new WaitUntil(func);
            if (www.isHttpError || www.isNetworkError)
            {
                Utility.Logger.errorlog("Error2 : " + www.error);
                yield break;
            }

            var content = DownloadHandlerTexture.GetContent(www);
            if(content == null)
            {
                Utility.Logger.errorlog("Error getting image from web");
            }
            var sprite2 = Sprite.CreateSprite(content,
                new Rect(0f, 0f, content.width, content.height), new Vector2(0f, 0f), 100000f, 1000u,
                SpriteMeshType.FullRect, Vector4.zero, false);
            sprite = sprite2;
        }
        public static IEnumerator LoadTextureFromUrl(Texture2D sprite, string url)
        {
            courtinedone = false;
            Utility.Logger.log("Downloading " + url);

            var www = UnityWebRequestTexture.GetTexture(url);
            _ = www.downloadHandler;
            var asyncOperation = www.SendWebRequest();
           

            Func<bool> func = () => asyncOperation.isDone;
            yield return new WaitUntil(func);
            if (www.isHttpError || www.isNetworkError)
            {
                Utility.Logger.errorlog("Error2 : " + www.error);
                yield break;
            }

            var content = DownloadHandlerTexture.GetContent(www);
            if (content == null)
            {
                Utility.Logger.errorlog("Error getting image from web");
            }
            Utility.Logger.log("Downloaded " + url);
            sprite = content;
            courtinedone = true;
            Client.Hacks.DarkMode.LoadedAssets++;
        }
        private static Sprite LoadSprite(string sprite)
        {
            Sprite sprite2 = Bundle.LoadAsset_Internal(sprite, Il2CppType.Of<Sprite>()).Cast<Sprite>();
            sprite2.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            Utility.Logger.log("Loaded asset " + sprite);
            return sprite2;
        }
        private static Sprite loadgameSprite(AssetBundle bundle, string AssetName)
        {
            if (AssetName == " ")
            {
                GameObject.Instantiate(bundle.mainAsset);
                return null;
            }
            else
            {
                //GameObject go=bundle.LoadAsset<GameObject>(AssetName);
                Sprite go = bundle.LoadAsset(AssetName) as Sprite;
                return go;
            }
        }
        private static Texture2D LoadTexture(string Texture, AssetBundle bundle)
        {
            Texture2D Texture2 = bundle.LoadAsset_Internal(Texture, Il2CppType.Of<Texture2D>()).Cast<Texture2D>();
            Texture2.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            
            Utility.Logger.log("Loaded texture" + Texture);
            return Texture2;
        }

        private static Material LoadMaterial(string Material, AssetBundle bundle)
        {
            Material Texture2 = bundle.LoadAsset_Internal(Material, Il2CppType.Of<Material>()).Cast<Material>();
            Texture2.hideFlags |= HideFlags.DontUnloadUnusedAsset;

            Utility.Logger.log("Loaded material" + Material);
            return Texture2;
        }
        
        private static GameObject LoadPrefab(string go, AssetBundle bundle) {
            GameObject go2 = bundle.LoadAsset_Internal(go, Il2CppType.Of<GameObject>()).Cast<GameObject>();
            go2.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            Utility.Logger.log("Loaded gameobject" + go);

            return go2;
        }
        

        public static void Init() {
            Utility.Logger.log("Loading Assets ");
            MelonCoroutines.Start(LoadResources());
            //MelonLoader.MelonCoroutines.Start(Client.Assets.Resources.LoadSprieFromUrl(clientlogo, "https://doc-00-a0-docs.googleusercontent.com/docs/securesc/orfp9fc5rrf2dc17kjlitcd6iipkkauo/aatm273j4egn44fvf5c8drkopdeo3id8/1652480175000/01928027953858924923/01928027953858924923/1yakLEZ0wJznU_WZPo6ad7RCDLjna-s58?e=download&ax=ACxEAsZuPU2SDu6DdVCINkw73rsEJe79udmCBRpzZ2NVG_qu9SiFLY3pwtEteqpwDKIM2353C_9TWAv3MadNIUxSEZCqiJ5AI37tiouot-i7e0PkjKu078HBWTZV_OM4iBYqg3j_aErKzsf4F5nvciHMccFNOvvs7N1Tndv7ZsFngxZTiDulDO6fm41iZJ0_tQPvENNWcArfwoHILP6vqnp10_msqA42v4DxW03lfefvwksdNkvVjsJRNcefd4U49v15cRvdOSIlp7Eysx3VI6tjR8e7xn7jvo5Z0hGbuLGQVVniRFFOp1Dvc8yb_fvh9uOx0W10A6xG4FZajxz5tBt0hxRzAvb5wFceXNplgeOLK33gLaaWesFuy1r_foY7jYM9N7mJiUvy8sJWcYNbd6nFwwhPynet1lk6yQKyyCmExvgpKqamWPkgv_aTPuxXWufNnYOTkCqgv1u0C7jQtj7wNFbLUu9duc6D3Nz21U6chzFAe3YCApNuF-JXUS2Hoy4yjbvGIqG9c06TaeSZIxrmEjj0tTvkEzgXOj40-Jbt9ScBm5dWomQUg71XIIgINL9jsjLbUsTwxjSL1qYZZM4sHUBwYH-HfD9FFI-0ml87WFeHLdOAGxq53HvKv8vDHskl_s8ESoOYhDbpiGbfCV_185olYi3FcErqvKPbwHQWgtg6D7fMpAoU66KruX9ss7hUUK6nOlDYJqxnuuGSFTpKsHiVDJYdRCyJMGPxgpUssgwIvbhDd1XsRsfr6-0B3HId5Nz3HyvwbjcmqN-WuMxt-32QKJ8O7VNk0wJ39v1tCiQg1MUA3udmsybO&authuser=0&nonce=t4uh111b0fesg&user=01928027953858924923&hash=m5sd54q3pa4c7hohkv2qsmqu7k9s8cti"));
            //MelonLoader.MelonCoroutines.Start(Client.Assets.Resources.LoadSprieFromUrl(clientlogo, "https://drive.google.com/uc?export=download&id=1yakLEZ0wJznU_WZPo6ad7RCDLjna-s58"));


        }

        private static IEnumerator LoadResources()
        {
        // //// Came from UIExpansionKit (https://github.com/knah/VRCMods/blob/master/UIExpansionKit)
        // Utility.Logger.log("Loading AssetBundle...");
        // string path = Path.Combine(Path.Combine(System.Environment.CurrentDirectory, "DaydreamAssets"), "daydreambundle");
        // Utility.Logger.log(path + " Exists: " + File.Exists(path));
        // AssetBundleCreateRequest bundleLoadRequest = AssetBundle.LoadFromFileAsync(path,);
        // yield return bundleLoadRequest;

        // if (bundleLoadRequest == null)
        // {
        //     Utility.Logger.errorlog("Failed to request AssetBundle!");
        //     yield break;
        // }
        // AssetBundle myLoadedAssetBundle = bundleLoadRequest.assetBundle;
        // if (myLoadedAssetBundle == null)
        //{
        //     Utility.Logger.errorlog("Failed to load AssetBundle!");
        //    yield break;
        // }
        // try { Utility.Logger.errorlog("Main Asset: " + myLoadedAssetBundle.mainAsset.name); } catch { Utility.Logger.errorlog(" Cant get main asset"); };

        // try
        // {
        //     for (int i = 0; i < myLoadedAssetBundle.GetAllAssetNames().Length; i++)
        //     {
        //         Utility.Logger.log(myLoadedAssetBundle.GetAllAssetNames()[i]);
        //     }
        // }
        // catch
        // {

        // }

        // Utility.Logger.log("Getting assets");
        // GameObject.Instantiate(Bundle.LoadAssetAsync<GameObject>("Assets/BundledAssets/notification.prefab").asset as GameObject);
        // Bundle.Unload(false);
        // yield break;
       // UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle("https://raw.githubusercontent.com/GITDragonxd07/UnityClientDll/main/daydreambundle.assets", 0);
       UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(Path.Combine(Environment.CurrentDirectory, "DaydreamAssets") + "/daydreambundle.assets", 0); // "D:/Unity/dd/Assets/StreamingAssets/daydreambundle.assets
            yield return uwr.SendWebRequest();
                while (!uwr.isDone)
                {
                    yield return null;
                 }
                if (uwr.isNetworkError || uwr.isHttpError)
                {
                    Utility.Logger.errorlog(uwr.error);
                }
                else
                {
                
                // Get downloaded asset bundle
                Utility.Logger.log("Getting assetbundle");
                 AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);

                if (bundle == null)
                {
                    Utility.Logger.errorlog("bundle error");
                }
                else
                {
                    Utility.Logger.log("Asset bundle contains: ");
                    foreach (string name in bundle.GetAllAssetNames())
                    {
                        Utility.Logger.log($"name={name}");
                    }
                    //skybox = Material.Instantiate(bundle.LoadAsset<Material>("Assets/Bundle/Skybox.mat"));
                    back = LoadTexture("Assets/Bundle/back.png", bundle);
                    down = LoadTexture("Assets/Bundle/down.png", bundle);
                    front = LoadTexture("Assets/Bundle/front.png", bundle);
                    left = LoadTexture("Assets/Bundle/left.png", bundle);
                    right = LoadTexture("Assets/Bundle/right.png", bundle);
                    up = LoadTexture("Assets/Bundle/up.png", bundle);
                    notificationui = LoadPrefab("Assets/Bundle/Panel.prefab", bundle);

                    loadedassets = true;
                }
                
                }
                
            }

        }
    }



