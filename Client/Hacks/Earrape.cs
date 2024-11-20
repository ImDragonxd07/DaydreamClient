using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daydream.Client.Hacks
{
    internal class Earrape
    {
        public static void toggle(bool value)
        {
            Classes.Daydream.HackUpdate("Earrape mic", value);
            earrape(value);
        }
        private static void earrape(bool enabled)
        {
            if (enabled == true)
            {
                USpeaker.field_Internal_Static_Single_1 = float.MaxValue;
            }
            else
            {
                USpeaker.field_Internal_Static_Single_1 = 1;

            }
        }
    }
}
