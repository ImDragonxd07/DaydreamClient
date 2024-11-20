using Il2CppSystem.Text.RegularExpressions;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daydream.Client.Utility
{
    internal class Logger : MelonMod
    {
        public static bool owo = false;
        private static void melonlog(ConsoleColor col, string txt)
        {
            if(owo == true)
            {
                MelonLogger.Msg(col, Utility.owo.Owoifier.Owoify(txt));
            }
            else
            {
                MelonLogger.Msg(col, txt);
            }
        }
    public static void log(string msg)
        {
            melonlog(ConsoleColor.Cyan, "[Core Script] " + msg);
        }
        public static void errorlog(string msg)
        {
            melonlog(ConsoleColor.Red, "[Error] " + msg);
        }
        public static void externallog (string msg)
        {
            melonlog(ConsoleColor.DarkMagenta, "[External Script] " + msg);
        }
        public static void discordlog(string msg)
        {
            melonlog(ConsoleColor.Magenta, "[Discord] " + msg);
        }
        public static void networklog(string msg)
        {
            melonlog(ConsoleColor.Yellow, "[Networking] " + msg);
        }
        public static void rawlog(string msg)
        {
            Console.WriteLine(msg);
        }
        public static void startmsg()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Daydream.");
        }
    }
}
