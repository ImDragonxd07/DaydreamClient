using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MelonLoader;
using MelonLoader.TinyJSON;

using UnityEngine;
using UnityEngine.UI;

namespace Daydream.Client.Ui
{

    public class Manager
    {
        public VRChatUtilityKit.Ui.ToggleButton CreateToggle(Action<bool> func, string name, string gameobject, string tooltip, bool DefultValue = false)
        {

            string data = Utility.SaveData.getdata();
            bool isebabled = DefultValue;

            try
            {
                string[] lines = data.Split(new string[] { "}," }, StringSplitOptions.None);
                foreach (string line in lines)
                {
                    string newline = "";
                    if (!line.EndsWith("}"))
                    {
                        newline = line + "}";
                    }
                    else
                    {
                        newline = line;
                    }
                    var newjson = JSON.Load(newline);
                    if (newjson["buttonname"] == name)
                    {
                        isebabled = bool.Parse(newjson["state"]);
                        Utility.Logger.log("loading "+ newjson["state"] + " state for " + newjson["buttonname"]);
                    }
                }
            }
            catch
            {

            }
            VRChatUtilityKit.Ui.ToggleButton button = new VRChatUtilityKit.Ui.ToggleButton((state) => toggle(func, state, name, null,true), GameObject.Find("UserInterface/MenuContent/Screens/Settings/OtherOptionsPanel/TooltipsToggle/Background/Checkmark").GetComponent<Image>().sprite, null, name, gameobject, tooltip, "");
            toggle(func, isebabled, name, button,true);
            return button;

        }
        public VRChatUtilityKit.Ui.ToggleButton TempToggle(Action<bool> func, string name, string gameobject, string tooltip)
        {
            VRChatUtilityKit.Ui.ToggleButton button = new VRChatUtilityKit.Ui.ToggleButton((state) => toggle(func, state, name, null, false), GameObject.Find("UserInterface/MenuContent/Screens/Settings/OtherOptionsPanel/TooltipsToggle/Background/Checkmark").GetComponent<Image>().sprite, null, name, gameobject, tooltip, "");
            toggle(func, false, name, button, false);
            return button;
        }
        private static void toggle(Action<bool> func, bool state, string buttonname, VRChatUtilityKit.Ui.ToggleButton button, bool save)
        {
            func?.Invoke(state);
            if (button != null)
            {
                button.ToggleComponent.isOn = state;
            }
            if(save == true)
            {
                Utility.Logger.log("Saving state " + state);

                foreach (var objet in Utility.SaveData.data.ToList())
                {
                    if (objet.buttonname == buttonname)
                    {
                        Utility.SaveData.data.Remove(objet);
                    }
                }
                Utility.SaveData.data.Add(new Utility.TestStruct() { buttonname = buttonname, state = state });

            }
            Utility.Logger.log(buttonname + " " + state);
        }
    }

}
