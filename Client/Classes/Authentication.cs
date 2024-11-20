using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Daydream.Client.Classes
{
    internal class Authentication
    {
        public static bool IsBanned()
        {
            //string ip = GetIP();
            //WebClient client = new WebClient();
            //string reply = client.DownloadString("https://raw.githubusercontent.com/GITDragonxd07/UnityClientDll/main/bans.txt").Replace("\"", "");
            //string[] subs = reply.Split(',');

            //foreach (string sub in subs)
            //{
            //    if(sub == ip)
            //    {
            //        Utility.Logger.networklog("You are banned from daydream network");
            //        return true;

            //    }
                
            //}
            //Utility.Logger.networklog("Logged into daydream authenticator as: " +  ip);
            return false;
        }
        public static string GetIP()
        {
            string externalIP = "";
            externalIP = new WebClient().DownloadString("http://checkip.dyndns.org/");
            externalIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
                                           .Matches(externalIP)[0].ToString();
            return externalIP;
        }
    }
}
