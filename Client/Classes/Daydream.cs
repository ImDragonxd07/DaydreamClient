using Harmony;
using MelonLoader;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib;
using UnityEngine;
using VRC;
using VRC.SDKBase;
using VRC.Utility;

using UnityEngine.UI;

using static UnityEngine.UI.Button;
using VRChatUtilityKit.Components;
using VRC.UI.Elements.Controls;
using System.Reflection;
using VRC.Core;
using UnityEngine.Networking;
using MelonLoader.TinyJSON;
using System.IO;
using RenderHeads.Media.AVProMovieCapture;

namespace Daydream.Client.Classes
{
    internal class Daydream : MelonMod
    {
        public static GameObject localplayer;
        public static float Impulse;
        public static bool GameLoaded = false;
        public static bool sceneloaded = false;
        public static HarmonyInstance daydreampatch;
        public static string Name = "Daydream (BETA)";
        public static bool isphotonbot = false;
        public static string BaseWebsiteurl = "https://www.daydream.tk";
        public static string version = "";
        public static bool isadmin = false;
        private static IEnumerator getversion()
        {

            UnityWebRequest www = UnityWebRequest.Get("https://api.github.com/repos/GITDragonxd07/UnityClientDll/releases/latest");
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                MelonLogger.Log(www.error);
            }
            else
            {
                var Data = JSON.Load(www.downloadHandler.text);
                version = Data["name"];
            }
        }
        public static bool Load = false;
        //public override void OnApplicationStart() // Runs after OnApplicationStart.
        public static void OnApplicationStart()
        {
            if (File.Exists(MelonHandler.ModsDirectory + "/DaydreamLoader.dll") && Classes.Authentication.IsBanned() == false)
            {
                Load = true;
            }
            else
            {
                Utility.Logger.errorlog("Couldent find file DaydreamLoader.dll");
            }
            if(Load == true) {
                Classes.Updater.update();
        //        daydreampatch = new HarmonyInstance("DaydreamPatch");
        //        typeof(RoomManager).GetMethods(BindingFlags.Public | BindingFlags.Static)
        //.Where(m => m.Name.StartsWith("Method_Public_Static_Boolean_ApiWorld_ApiWorldInstance_String_Int32_"))
        //.ToList().ForEach(m => daydreampatch.Patch(m, prefix: new HarmonyMethod(typeof(Daydream).GetMethod(nameof(OnWorldFullLoad), BindingFlags.Public | BindingFlags.Static))));
            }

            //Utility.Title.title(); //  RANDOM MESSAGE
            //Process.Start("https://discord.gg/SpZSH5Z");
        }
        public static void continuestart()
        {
            Communication.DaydreamNetwork.load();
            Console.Write("Installing RuntimeInjection");
            //Classes.RuntimeInjection.load("Console.Write(\"Installed RuntimeInjection\")");
            Bots.AppBot.OnStart();
            if (isphotonbot == false)
            {
                Utility.Logger.networklog("Not photon bot");

            }
            else
            {
                Utility.Logger.networklog("Is photon bot");
            }
            Utility.Logger.networklog("Current spotify " );
            MelonCoroutines.Start(getversion());
            Hacks.SoftClone.Load();
            Assets.Resources.Init();
            Hacks.RpcLog.init();
            Utility.SaveData.startclient();
            Hacks.playerinfo.Start();
            Classes.PhotonManager.onclientload();
            //Ui.Ui.selectinmenuUI();
            Ui.TabUi.OnApplicationStart();

            Utility.Logger.log("Started");
            Utility.Logger.startmsg();

        }
        public static void OnWorldFullLoad(ApiWorld __0, ApiWorldInstance __1)
        {
            if(Load == true)
            {
                Utility.Logger.networklog($"https://vrchat.com/home/launch?worldId={__0.id}&instanceId={__1.instanceId}");
            }
        }
        public override void OnSceneWasLoaded(int buildindex, string sceneName) // Runs when a Scene has Loaded and is passed the Scene's Build Index and Name.
        {
            if(Load == true)
            {
                sceneloaded = false;
                MelonCoroutines.Start(Hacks.DarkMode.loadcustomaudio());
                Protections.PhotonClient.AntiCrash.OnSceneWasLoaded(buildindex, sceneName);
                if (GameLoaded == false)
                {
                    Utility.Logger.log("Installed");
                    Ui.Ui.build();
                    Ui.Ui.HackUi(true, "", false);
                    GameLoaded = true;
                    localplayer = Networking.LocalPlayer.gameObject;
                }
                if (isphotonbot == true)
                {
                    AudioListener.pause = true;
                    AudioListener.volume = 0; // MUTE THE GAME IF BOT
                }

                Utility.Logger.log("Loaded: " + buildindex.ToString() + " | " + sceneName);
                Utility.Logger.log("Loading Scene");
                Ui.Ui.Notification("Welcome to " + sceneName);
                MelonCoroutines.Start(Utility.Enum.waitForSceneLoad(buildindex));
                sceneloaded = true;
                Utility.Logger.log("Loaded Scene");
                Ui.Ui.tabui(sceneName);
                //Ui.MenuItem.OnUiManagerInit();
            }
        }
        public static int currentscene = 0;
        public static bool firstjoin = false;
        public static IEnumerator startlogin(string s1, string s2)
        {
            Utility.Logger.errorlog("Login no longer supported post EAC");
            return null;
            //while (VRCUiManager.prop_VRCUiManager_0 == null)
            //    yield return null;
            //Client.Utility.LoginManager.login(s1,s2);
        }
        public static Discord discord;
        public static void onfirstjoin()
        {
           if(Load == true)
            {
                firstjoin = true;

                discord = new Classes.Discord();
                discord.Initialize();
                Communication.DaydreamNetwork.CheckAdmin();
                Networking.GoToRoom(Bots.AppBot.worldid.Trim());
                Client.Avatar.AvatarSearch.networkclient();
                Utility.Logger.log("First Join");
                Ui.TabUi.VRChat_OnUiManagerInit();
                Ui.TabUi.PlayerMenu();
                Client.Ui.Ui.AvatarSearchUI();
                var loading = GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup").AddComponent<EnableDisableListener>();
                loading.OnEnableEvent += RunLoadingscreen;
                Hacks.Freecam.Freecam.LoadMain();
            }

        }
        private static void GameobjectActive()
        {
            if(GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup").active == true)
            {
                RunLoadingscreen();
            }
        }
        public override void OnSceneWasInitialized(int buildindex, string sceneName) // Runs when a Scene has Initialized and is passed the Scene's Build Index and Name.
        {
            if(Load == true)
            {

                Client.Optimizations.cleanup.init();
                currentscene = buildindex;
                Utility.Logger.log("Installed: " + buildindex.ToString() + " | " + sceneName);
                if (buildindex != -1)
                {
                    RunLoadingscreen();
                }
            }
        }

        public static void RunLoadingscreen()
        {
           if(Load == true)
            {
                MelonCoroutines.Start(Hacks.DarkMode.setskybox());
                Hacks.DarkMode.playcustomloadingmusic();
                Hacks.DarkMode.run();
            }
        }

        public override void OnUpdate() // Runs once per frame.
        {
            if(Load == true)
            {
                //if(currentscene == -1)
                //{
                //    Client.Optimizations.cleanup.update();
                //}
                Bots.AppBot.OnUpdate();
                Client.Ui.Ui.updateavatar();
                Hacks.playerinfo.UpdateNameplates();
                Utility.Keybinds.runkeybinds();
                Hacks.Freecam.Freecam.Update();
            } 
        }

        
        public static void HackUpdate(string name,bool val)
        {
            Utility.Logger.log("Enabling Cheat " + name);
            Utility.Logger.log("Found Localplayer " + localplayer.transform.name );
            Ui.Ui.HackUi(false, name, val);
        }

        public override void OnApplicationQuit() // Runs when the Game is told to Close.
        {
            if(Load == true)
            {
                Networking.GoToRoom("v1:a1");
                if (isphotonbot == false)
                {
                    Utility.SaveData.exitgame();
                }
                else
                {
                }
                Utility.Logger.log("Game Close");
            }
        }

        public override void OnPreferencesSaved() // Runs when Melon Preferences get saved.
        {
            if(Load == true)
            {
                Utility.Logger.log("Saved User Data");
                Utility.Logger.log("Quick Exit");
                //taskkill /IM "process name" /F
                Application.Quit();
            }
        }

        public override void OnPreferencesLoaded() // Runs when Melon Preferences get loaded.
        {
            if(Load == true)
            {
                Utility.Logger.log("Loaded User Data");
            }
        }
    }
}
