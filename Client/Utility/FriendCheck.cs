using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC.Core;
using VRC.SDKBase;

namespace Daydream.Client.Utility
{
    internal class FriendCheck
    {
        List<String> friends = new List<String>();
        public static void start()
        {
            // CURRENTLY USELESS BECAUSE OF NO FRIEND FUNCTIONS
            APIUser.FetchFriends();
        }
        public static void update()
        {

        }
    }
}
