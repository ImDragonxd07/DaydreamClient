using Daydream.Client.Hacks;
using Daydream.Client.Ui.Pages;
using Il2CppSystem.Collections.Generic;
using Oculus.Platform;
using System;
using System.Linq;
using System.Reflection;
using System.Web.WebPages;
using Transmtn;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.DataModel;
using VRC.SDK.Internal.Tutorial;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;
using VRC.UI;

namespace Daydream.Client.Utility
{
    public static class Extentions
    {
        public static List<VRCPlayerApi> GetPlayers()
        {
            return VRCPlayerApi.AllPlayers;
        }

        public static VRCPlayerApi getPlayerFromPlayerlist(string userID)
        {
            VRCPlayerApi.GetPlayerById(userID.AsInt());
            //foreach (var player in PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0)
            //{
            //    if (player.prop_APIUser_0 != null)
            //    {
            //        if (player.prop_APIUser_0.id.Equals(userID))
            //            return player;
            //    }
            //}

            return null;
        }


        //public static VRCPlayerApi GetVRCPlayer(this VRCPlayerApi player)
        //{
        //    return player.GetVRCPlayer();
        //}

        //public static APIUser GetAPIUser(this VRCPlayerApi player)
        //{
        //    return player.GetAPIUser();
        //}

        //public static ApiAvatar GetApiAvatar(this VRCPlayerApi player)
        //{
        //    return player.GetApiAvatar();
        //}
        
        //public static Player GetPlayer(this VRCPlayer vrcPlayer)
        //{
        //    return vrcPlayer._player;
        //}

        //public static Playe GetPlayerNet(this VRCPlayer vrcPlayer)
        //{
        //    return vrcPlayer._playerNet;
        //}

        public static GameObject GetAvatarObject(this VRCPlayerApi vrcPlayer)
        {
            return Networking.LocalPlayer.GetAvatarObject();
        }


        //public static string GetUserID(this IUser iUser)
        //{
        //    return iUser.prop_String_0;
        //}


        public static bool IsStaff(this APIUser user)
        {
            if (user.hasModerationPowers)
                return true;
            if (user.developerType != APIUser.DeveloperType.None)
                return true;
            return user.tags.Contains("admin_moderator") || user.tags.Contains("admin_scripting_access") ||
                   user.tags.Contains("admin_official_thumbnail");
        }

        private static MethodInfo _reloadAvatarMethod;
        private static MethodInfo LoadAvatarMethod
        {
            get
            {
                if (_reloadAvatarMethod == null)
                {

                    //_reloadAvatarMethod = typeof(VRCPlayer).GetMethods().First(mi => mi.Name.StartsWith("Method_Private_Void_Boolean_") && mi.Name.Length < 31 && mi.GetParameters().Any(pi => pi.IsOptional) && VRChatUtilityKit.Utilities.XrefUtils.CheckUsedBy(mi, "ReloadAvatarNetworkedRPC"));
                }

                return _reloadAvatarMethod;
            }
        }

        private static MethodInfo _reloadAllAvatarsMethod;
        private static MethodInfo ReloadAllAvatarsMethod
        {
            get
            {
                if (_reloadAllAvatarsMethod == null)
                {
                    //_reloadAllAvatarsMethod = typeof(VRCPlayer).GetMethods().First(mi => mi.Name.StartsWith("Method_Public_Void_Boolean_") && mi.Name.Length < 30 && mi.GetParameters().All(pi => pi.IsOptional) && VRChatUtilityKit.Utilities.XrefUtils.CheckUsedBy(mi, "Method_Public_Void_", typeof(FeaturePermissionManager)));// Both methods seem to do the same thing;
                }

                return _reloadAllAvatarsMethod;
            }
        }
        public static void ReloadAvatar(this VRCPlayerApi instance)
        {
            LoadAvatarMethod.Invoke(instance, new object[] { true }); // parameter is forceLoad and has to be true
        }

        public static void ReloadAllAvatars(this VRCPlayerApi instance, bool ignoreSelf = false)
        {
            ReloadAllAvatarsMethod.Invoke(instance, new object[] { ignoreSelf });
        }
       
        public static APIUser GetUserFromId(string id)
        {
            return APIUser.FetchUser(id,Il2CppSystem.Action<APIUser>(),);
        }
        public static void ChangeToAvatar(string avatarId)
        {
            
            PageAvatar pageAvatar = GameObject.Find("Screens").transform.Find("Avatar").GetComponent<PageAvatar>();
            pageAvatar.avatar.apiAvatar = new ApiAvatar
            {
                id = AvatarId
            };
            pageAvatar.ChangeToSelectedAvatar();


            //instance for compatability

            //var apiModelContainer = new ApiModelContainer<ApiAvatar>
            //{
            //    OnSuccess = new Action<ApiContainer>(c =>
            //    {
            //        var pageAvatar = Resources.FindObjectsOfTypeAll<PageAvatar>()[0];
            //        var apiAvatar = new ApiAvatar
            //        {
            //            id = avatarId
            //        };
            //        pageAvatar.field_Public_SimpleAvatarPedestal_0.field_Internal_ApiAvatar_0 = apiAvatar;
            //        pageAvatar.ChangeToSelectedAvatar();
            //    })
            //};
            //API.SendRequest($"avatars/{avatarId}", 0, apiModelContainer, null, true, true, 3600f, 2);

        }
    }
}
