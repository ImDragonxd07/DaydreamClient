using Daydream.Client.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.Core;
using VRC.SDKBase;

namespace Daydream.Client.Hacks
{

    internal class CopyAvId
    {
        public static void copy()
        {
                    foreach (VRCPlayerApi plr in Utility.Extentions.GetPlayers())
                    {
                        Utility.Extentions.ChangeToAvatar(plr.api().id);
                        TextEditor te = new TextEditor();
                        te.content = new GUIContent(plr..id);
                          
                        te.SelectAll();
                        te.Copy();
                    }

            }
        }
    }
}
