using Il2CppSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Daydream.Client.Utility
{
    internal class Title
    {
        [DllImport("user32.dll", EntryPoint = "SetWindowText")]

        public static extern bool SetWindowText(System.IntPtr hwnd, System.String lpString);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern System.IntPtr FindWindow(System.String className, System.String windowName);
        private static System.String[] txt = {"Made with love","9 + 10 = 21","I swear i have real friends","Todays a good day to do nothing","Press alt + f4 for free VRChat plus","Dont ban me tupper ;(","your average client"};
        public static void title()
        {
            System.Random rnd = new System.Random();

            //Get the window handle.
            //Set the title text using the window handle.
            SetWindowText(FindWindow(null, "VRChat"), "VRChat: " + txt[rnd.Next(0, txt.Length)]);
        }
    }
}
