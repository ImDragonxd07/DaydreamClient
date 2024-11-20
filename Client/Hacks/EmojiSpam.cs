using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib;
using VRC.SDKBase;

namespace Daydream.Client.Hacks
{
    internal class EmojiSpam
    {
        public static void HookSpawnEmojiRPC()
        {
            Networking.RPC(RPC.Destination.All,Classes.Daydream.localplayer, "SpawnEmojiRPC",new[] { new global::Il2CppSystem.Int32 { m_value = 21 }.BoxIl2CppObject() });
        }
    }
}
