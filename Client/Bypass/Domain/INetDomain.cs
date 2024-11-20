using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Daydream.Client.Bypass.Domain
{
    [ComImport, Guid("E86B87C8-5FC2-442E-A2AB-EAB2E594FEAE"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface INetDomain
    {
        void Initialize();
        void OnApplicationStart();
        void MinHook_CreateInstance(IntPtr mVRC_CreateHook, IntPtr mVRC_RemoveHook, IntPtr mVRC_EnableHook, IntPtr mVRC_DisableHook);
    }
}
