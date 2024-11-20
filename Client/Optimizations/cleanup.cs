using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.Scripting;
namespace Daydream.Client.Optimizations
{
    internal class cleanup
    {
        
        // UNITY ALREADY DOES THIS
        //private static float dt = 0F;
        //private static int totalframes = 0;
        public static void init()
        {
            GarbageCollector.CollectIncremental(5);
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = -1;
            Camera.main.allowDynamicResolution = true;
            Camera.main.useOcclusionCulling = true;
            Utility.Logger.log("loaded optimizations");
        }
        //public static void update()
        //{

        //    Utility.Logger.log("running at " + Mathf.Round(displayValue) + "/" + Mathf.Round( totalframes/framenum ));
        //    if (  ) // lagging
        //    {
        //        Utility.Logger.log("Lagged at " +  + " fps");
        //    }
        //    else
        //    {
        //        GarbageCollector.CollectIncremental(3);
        //    }
        //}
    }
}
