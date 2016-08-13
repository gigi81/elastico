using System;
using System.Diagnostics;
using System.Collections;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace Elasticsearch.Powershell.Tests
{
    class NetApi32
    {
        public enum SHARE_TYPE : ulong
        {
            STYPE_DISKTREE = 0,
            STYPE_PRINTQ = 1,
            STYPE_DEVICE = 2,
            STYPE_IPC = 3,
            STYPE_SPECIAL = 0x80000000,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SHARE_INFO_502
        {
            [MarshalAs(UnmanagedType.LPWStr)]
            public string shi502_netname;
            public uint shi502_type;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string shi502_remark;
            public Int32 shi502_permissions;
            public Int32 shi502_max_uses;
            public Int32 shi502_current_uses;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string shi502_path;
            public IntPtr shi502_passwd;
            public Int32 shi502_reserved;
            public IntPtr shi502_security_descriptor;
        }

        [DllImport("Netapi32.dll")]
        public static extern int NetShareAdd(
                [MarshalAs(UnmanagedType.LPWStr)]
                string strServer,
                Int32 dwLevel,
                IntPtr buf,
                IntPtr parm_err);
    }

    [Flags]
    public enum SharePermissions
    {
        None = 0,
        Read = 1,
        Write = 2,
        Create = 4,
        Exec = 8,
        Delete = 0x10,
        Atrib = 0x20,
        Perm = 0x40,
        All = Read +
              Write +
              Create +
              Exec +
              Delete +
              Atrib +
              Perm,
        Group = 0x8000
    }

    internal class SambaShareApi
    {
        public static void CreateShare(string strPath,
                                       string strShareName,
                                       string strShareDesc,
                                       bool bAdmin,
                                       SharePermissions permissions,
                                       string strServer = null)
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/bb525384(v=vs.85).aspx
            var share = new NetApi32.SHARE_INFO_502();
            share.shi502_netname = strShareName;
            share.shi502_type = (uint)NetApi32.SHARE_TYPE.STYPE_DISKTREE;
            if (bAdmin)
            {
                share.shi502_type = (uint)NetApi32.SHARE_TYPE.STYPE_SPECIAL;
                share.shi502_netname += "$";
            }
            share.shi502_permissions = (int)permissions;
            share.shi502_path = strPath;
            share.shi502_passwd = IntPtr.Zero;
            share.shi502_remark = strShareDesc;
            share.shi502_max_uses = -1;
            share.shi502_security_descriptor = IntPtr.Zero;

            var buffer = Marshal.AllocCoTaskMem(Marshal.SizeOf(share));
            Marshal.StructureToPtr(share, buffer, false);

            var nRetValue = NetApi32.NetShareAdd(GetTargetServer(strServer), 502, buffer, IntPtr.Zero);

            Marshal.FreeCoTaskMem(buffer);

            if (nRetValue != 0)
                throw new Win32Exception(nRetValue);
        }

        private static string GetTargetServer(string strServer)
        {
            if (String.IsNullOrEmpty(strServer) || strServer[0] == '\\')
                return strServer;

            return "\\\\" + strServer;
        }
    }
}