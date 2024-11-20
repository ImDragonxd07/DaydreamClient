
 using System;
using System.IO;
using UnityEngine.Networking;
using System.Collections;
using MelonLoader;
using I2.Loc.SimpleJSON;
using Il2CppNewtonsoft.Json.Linq;
using System.Net;
using UnityEngine;
using System.Linq;
using Il2CppSystem.Threading.Tasks;
using System.Diagnostics;

namespace Daydream.Client.Classes
{
    internal class Updater
    {
        public static void update()
        {
            Utility.Logger.log("getting data");
            bool asssets = !Directory.Exists(Path.Combine(Environment.CurrentDirectory, "DaydreamAssets"));
            if (asssets)
            {
                Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "DaydreamAssets"));
                Utility.Logger.log(Path.Combine(Environment.CurrentDirectory, "DaydreamAssets") + "/ver.txt");
                File.CreateText(Path.Combine(Environment.CurrentDirectory, "DaydreamAssets") + "/ver.txt").Dispose();
                using (StreamWriter writer = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "DaydreamAssets") + "/ver.txt"))
                {
                    writer.WriteLine("none");
                }
                if(!File.Exists(Path.Combine(Environment.CurrentDirectory, "DaydreamAssets") + "/bots.txt"))
                {
                    using (StreamWriter writer = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "DaydreamAssets") + "/bots.txt"))
                    {
                        writer.WriteLine("email:password");
                    }
                }
            }
            MelonCoroutines.Start(getversion());
        }

        private static void downloadlatest(string ver)
        {
            if (Directory.Exists(MelonHandler.ModsDirectory + "/VRChatUtilityKit(daydream).dll"))
            {
                File.Delete(MelonHandler.ModsDirectory + "/VRChatUtilityKit(daydream).dll");
            }
            if(File.Exists(MelonHandler.ModsDirectory + "/ddwebserver.dll")){
                File.Delete(MelonHandler.ModsDirectory + "/ddwebserver.dll");

            }
            if (File.Exists(Path.Combine(Environment.CurrentDirectory, "DaydreamAssets") + "/daydreambundle.assets"))
            {
                File.Delete(Path.Combine(Environment.CurrentDirectory, "DaydreamAssets") + "/daydreambundle.assets");

            }
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            Utility.Logger.log("Downloading Required Asssets");

            client.DownloadFile("https://github.com/SleepyVRC/Mods/releases/latest/download/VRChatUtilityKit.dll", MelonHandler.ModsDirectory + "/VRChatUtilityKit(daydream).dll");
            
            client.DownloadFile("https://github.com/Er1807/VRCWS/releases/latest/download/VRCWSLibary.dll", MelonHandler.ModsDirectory + "/ddwebserver.dll");
            using (StreamWriter writer = new StreamWriter(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "DaydreamAssets")) + "/ver.txt"))
            {
                writer.WriteLine(ver);
            }
            if (File.Exists(Path.Combine(Environment.CurrentDirectory, "DaydreamAssets") + "/bots.txt"))
            {
                File.Delete(Path.Combine(Environment.CurrentDirectory, "DaydreamAssets") + "/bots.txt");
            }
            using (StreamWriter writer = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "DaydreamAssets") + "/bots.txt"))
            {
                writer.WriteLine("email:password");
            }
            Utility.Logger.log("Downloaded");
            System.Windows.Forms.MessageBox.Show("Updated vrcutilitykit or webserver","Daydream");
            Process.Start(Environment.CurrentDirectory + "\\VRChat.exe", Environment.CommandLine.ToString());
            Application.ForceCrash(2);

        }
        private static bool restartrequired = false;
        public static void CheckForDll(string name, string parentpath, string url, bool forceupdate)
        {
            if (!File.Exists(parentpath + "/" + name) || forceupdate == true)
            {
                if(File.Exists(parentpath + "/" + name))
                {
                    File.Delete(MelonHandler.ModsDirectory + "/"+ name);
                }
                Utility.Logger.log(name + " does not exist");
                WebClient client = new WebClient();
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                Utility.Logger.log("Downloading Required Asssets");
                Utility.Logger.log("downloading " + name + " in " + parentpath + "/" + name);

                client.DownloadFile(url, parentpath + "/" + name);
                restartrequired = true;
            }
            else
            {
                Utility.Logger.log("found " + name);
            }
        }
        public static void DownloadAssetbundle()
        {
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            Utility.Logger.log("Downloading Required Asssets");

            client.DownloadFile(" https://raw.githubusercontent.com/GITDragonxd07/UnityClientDll/main/daydreambundle.assets", Path.Combine(Environment.CurrentDirectory, "DaydreamAssets") + "/daydreambundle.assets");
        }
        private static IEnumerator getversion()
        {
            Utility.Logger.log("done featching data");
            UnityWebRequest www = UnityWebRequest.Get("https://api.github.com/repos/SleepyVRC/Mods/releases/latest");



            yield return www.SendWebRequest();
            if (!string.IsNullOrEmpty(www.error))
            {
                Daydream.continuestart();

            }
            else
            {
                var myJObject = JObject.Parse(www.downloadHandler.text);
                string name = myJObject["name"].ToString();
                Utility.Logger.log("Waiting for DaydreamAssets");
                Utility.Logger.log("Finished");
                bool latest = false;
                try
                {
                    latest = System.IO.File.ReadAllLines(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "DaydreamAssets")) + "/ver.txt").First().ToString() == name.ToString();
                    Utility.Logger.log("Latest: " + latest);

                }
                catch
                {
                    // file will be created later
                }
                string utilpath = MelonHandler.ModsDirectory + ("/VRChatUtilityKit(daydream).dll");
                string path = MelonHandler.ModsDirectory + ("/ddwebserver.dll");

                Utility.Logger.log("Path: " + path.ToString());
                Utility.Logger.log("Exsists: " + File.Exists(path));
                Utility.Logger.log("Bots: " + File.Exists(Path.Combine(Environment.CurrentDirectory, "DaydreamAssets") + "/bots.txt"));

                //    if (!File.Exists(utilpath) || !latest || !File.Exists(path) || !File.Exists(Path.Combine(Environment.CurrentDirectory, "DaydreamAssets") + "/bots.txt") || !File.Exists(Path.Combine(Environment.CurrentDirectory, "DaydreamAssets") + "/ver.txt"))
                //{
                //    Utility.Logger.log("Downloading");
                //    downloadlatest(name);
                //}
                CheckForDll("VRChatUtilityKit(daydream).dll", MelonHandler.ModsDirectory, "https://github.com/SleepyVRC/Mods/releases/latest/download/VRChatUtilityKit.dll",false);
                CheckForDll("ddwebserver.dll", MelonHandler.ModsDirectory, "https://github.com/Er1807/VRCWS/releases/latest/download/VRCWSLibary.dll", false);
                CheckForDll("DiscordRPC.dll", Path.Combine(Environment.CurrentDirectory, "DaydreamAssets"), "https://raw.githubusercontent.com/GITDragonxd07/UnityClientDll/main/DiscordRPC.dll", false);
                CheckForDll("SpotifyAPI.Web.dll", Path.Combine(Environment.CurrentDirectory, "DaydreamAssets"), "https://raw.githubusercontent.com/GITDragonxd07/UnityClientDll/main/SpotifyAPI.Web.dll", false);
                CheckForDll("DynamicExpresso.Core.dll", Path.Combine(Environment.CurrentDirectory, "DaydreamAssets"), "https://raw.githubusercontent.com/GITDragonxd07/UnityClientDll/main/DynamicExpresso.Core.dll", false);
                CheckForDll("Microsoft.CodeAnalysis.CSharp.Scripting.dll", Path.Combine(Environment.CurrentDirectory, "DaydreamAssets"), "https://raw.githubusercontent.com/GITDragonxd07/UnityClientDll/main/Microsoft.CodeAnalysis.CSharp.Scripting.dll", false);
                CheckForDll("Microsoft.CodeAnalysis.dll", Path.Combine(Environment.CurrentDirectory, "DaydreamAssets"), "https://raw.githubusercontent.com/GITDragonxd07/UnityClientDll/main/Microsoft.CodeAnalysis.dll", false);
                CheckForDll("Microsoft.CodeAnalysis.Scripting.dll", Path.Combine(Environment.CurrentDirectory, "DaydreamAssets"), "https://raw.githubusercontent.com/GITDragonxd07/UnityClientDll/main/Microsoft.CodeAnalysis.Scripting.dll", false);
                CheckForDll("netstandard.dll", Path.Combine(Environment.CurrentDirectory, "DaydreamAssets"), "https://raw.githubusercontent.com/GITDragonxd07/UnityClientDll/main/netstandard.dll", false);
                CheckForDll("SpotifyAPI.Web.dll", Path.Combine(Environment.CurrentDirectory, "DaydreamAssets"), "https://raw.githubusercontent.com/GITDragonxd07/UnityClientDll/main/SpotifyAPI.Web.dll", false);

                if (!File.Exists(Path.Combine(Environment.CurrentDirectory, "DaydreamAssets") + "/daydreambundle.assets"))
                {
                    Utility.Logger.log("Downloading Assetbundle");
                    DownloadAssetbundle();
                }
                if(restartrequired == true)
                {
                    Process.Start(Environment.CurrentDirectory + "\\VRChat.exe", Environment.CommandLine.ToString());
                    Application.ForceCrash(2);
                }
                Daydream.continuestart();
            }

        }
    }

}
