using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daydream.Client.Bypass.Domain
{


    internal sealed class DomainManager : AppDomainManager, INetDomain
    {
        public static void ignore()
        {

        }

        public DomainManager() { }
        public override void InitializeNewDomain(AppDomainSetup appDomainInfo) { InitializationFlags = AppDomainManagerInitializationOptions.RegisterWithHost; }

        public void Initialize() => ignore();
        public void OnApplicationStart() => Classes.Daydream.OnApplicationStart();
        public void MinHook_CreateInstance(IntPtr mVRC_CreateHook, IntPtr mVRC_RemoveHook, IntPtr mVRC_EnableHook, IntPtr mVRC_DisableHook) => MinHook.CreateInstance(mVRC_CreateHook, mVRC_RemoveHook, mVRC_EnableHook, mVRC_DisableHook);
        // public void MH_PatchController(ref bool status, ref IntPtr target, ref IntPtr desc) => MinHook.MH_PatchController(ref status, ref target, ref desc);
    }
}
