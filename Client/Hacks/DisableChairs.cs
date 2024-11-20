using Harmony;
using System;
using VRC;
using VRC.Core;


namespace Daydream.Client.Hacks
{
    internal class DisableChairs
    {
        private static bool chairsdisabled = false;
        public static void toggle(bool val)
        {
            Classes.Daydream.HackUpdate("Disable Chairs", val);
            chairsdisabled = val;
        }
        public static void load()
        {
            Classes.Daydream.daydreampatch.Patch(typeof(VRC_StationInternal).GetMethod(nameof(VRC_StationInternal.Method_Public_Boolean_Player_Boolean_0)), new HarmonyMethod(typeof(DisableChairs), "PlayerCanUseStation"));
        }
        private static bool PlayerCanUseStation(ref bool __result, VRC_StationInternal __instance, Player __0, bool __1)
        {
            if (!chairsdisabled) return true;
            if (__0 == null) return true;

            __result = false;
            return false;
        }
    }
}
