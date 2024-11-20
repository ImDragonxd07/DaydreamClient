using MelonLoader;
using MelonLoader.TinyJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daydream.Client.Utility
{
    internal class SaveData
    {
        public static MelonPreferences_Category daydream;
        public static MelonPreferences_Entry<string> buttonstates;
        public static MelonPreferences_Entry<string> pinnum;


        public static void startclient()
        {
            daydream = MelonPreferences.CreateCategory("Daydream");
            buttonstates = daydream.CreateEntry<string>("buttonstates", "");
            pinnum = daydream.CreateEntry<string>("pin", "none");

        }
        public static void save(string preferance, string data)
        {
            MelonPreferences.SetEntryValue("Daydream", preferance, data);

            daydream.SaveToFile();

        }
        public static void deletesetting(string preferance)
        {
                        MelonPreferences.SetEntryValue("Daydream", preferance, "");

        }
        public static List<TestStruct> data = new List<TestStruct>();

        public static string getdata()
        {
            return MelonPreferences.GetEntry("Daydream", "buttonstates").GetValueAsString().Replace("[", "").Replace("]", "");

        }

        public static string getpin()
        {
            return MelonPreferences.GetEntry("Daydream", "pin").GetValueAsString();

        }
        public static void setpin(string pin)
        {
            Utility.SaveData.save("pin", pin);

        }
        public static void exitgame()
        {
            Utility.SaveData.save("buttonstates", JSON.Dump(data, EncodeOptions.None));
        }
        public static string loaddata(string preferance)
        {
            return MelonPreferences.GetEntry("Daydream", preferance).GetValueAsString();
        }
    }
    [Serializable]
    public struct TestStruct
    {
        public string buttonname;
        public bool state;
    }
}
