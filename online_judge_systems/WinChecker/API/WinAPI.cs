/*
 * WinChecker 0.3.spata-alpha
 * Authors: ZAccess, adm1nn
 * (c) 2016
 */
using System;
using System.Text;
using System.Runtime.InteropServices;

using static WinChecker.API.WinAPI;

namespace WinChecker.API
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class WinTrustFileInfo : IDisposable
    {
        public uint StructSize = (uint)Marshal.SizeOf(typeof(WinTrustFileInfo));
        public readonly IntPtr pszFilePath;
        public IntPtr hFile = IntPtr.Zero;
        public IntPtr pgKnownSubject = IntPtr.Zero;

        public WinTrustFileInfo(string filePath)
        {
            pszFilePath = Marshal.StringToCoTaskMemAuto(filePath);
        }
        public void Dispose()
        {
            Marshal.FreeCoTaskMem(pszFilePath);
        }
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class WinTrustData : IDisposable
    {
        uint StructSize = (uint)Marshal.SizeOf(typeof(WinTrustData));
        IntPtr PolicyCallbackData = IntPtr.Zero;
        IntPtr SIPClientData = IntPtr.Zero;
        WinTrustDataUIChoice UIChoice = WinTrustDataUIChoice.None;
        WinTrustDataRevocationChecks RevocationChecks = WinTrustDataRevocationChecks.None;
        readonly WinTrustDataChoice UnionChoice;
        readonly IntPtr FileInfoPtr;
        WinTrustDataStateAction StateAction = WinTrustDataStateAction.Ignore;
        IntPtr StateData = IntPtr.Zero;
        string URLReference = null;
        WinTrustDataProvFlags ProvFlags = WinTrustDataProvFlags.SaferFlag;
        WinTrustDataUIContext UIContext = WinTrustDataUIContext.Execute;

        public WinTrustData(string fileName)
        {
            WinTrustFileInfo wtfiData = new WinTrustFileInfo(fileName);
            FileInfoPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(WinTrustFileInfo)));
            Marshal.StructureToPtr(wtfiData, FileInfoPtr, false);
            UnionChoice = WinTrustDataChoice.File;
        }
        public void Dispose()
        {
            Marshal.FreeCoTaskMem(FileInfoPtr);
        }
    }
    public static class WinTrust
    {
        public const string WINTRUST_ACTION_GENERIC_VERIFY_V2 = "{00AAC56B-CD44-11d0-8CC2-00C04FC295EE}";

        public static bool VerifyEmbeddedSignature(string fileName)
        {
            WinTrustData wtd = new WinTrustData(fileName);
            Guid guidAction = new Guid(WINTRUST_ACTION_GENERIC_VERIFY_V2);
            WinVerifyTrustResult result = WinVerifyTrust(INVALID_HANDLE_VALUE, guidAction, wtd);
            bool ret = (result == WinVerifyTrustResult.Success);
            return ret;
        }
    }

    static class WinAPI
    {
        #region Privilege
        internal const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
        internal const int TOKEN_QUERY = 0x00000008;
        internal const int ANYSIZE_ARRAY = 1;
        internal const uint SE_PRIVILEGE_ENABLED_BY_DEFAULT = 0x00000001;
        internal const uint SE_PRIVILEGE_ENABLED = 0x00000002;
        internal const uint SE_PRIVILEGE_REMOVED = 0x00000004;
        internal const uint SE_PRIVILEGE_USED_FOR_ACCESS = 0x80000000;
        
        public const string SE_DEBUG_NAME = "SeDebugPrivilege";
        public const string SE_RESTORE_NAME = "SeRestorePrivilege";

        [StructLayout(LayoutKind.Sequential)]
        public struct LUID
        {
            public uint LowPart;
            public int HighPart;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct LUID_AND_ATTRIBUTES
        {
            public LUID Luid;
            public uint Attributes;
        }
        public struct TOKEN_PRIVILEGES
        {
            public uint PrivilegeCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = ANYSIZE_ARRAY)]
            public LUID_AND_ATTRIBUTES[] Privileges;
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetCurrentProcess();
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccess, out IntPtr TokenHandle);
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AdjustTokenPrivileges(IntPtr TokenHandle, [MarshalAs(UnmanagedType.Bool)]
        bool DisableAllPrivileges, ref TOKEN_PRIVILEGES NewState, uint BufferLengthInBytes,
            IntPtr PreviousState, IntPtr ReturnLengthInBytes);
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool LookupPrivilegeValue(string lpSystemName, string lpName, out LUID lpLuid);

        public static void SE_PRIVILEGE_ENABLE(string SE_PRIVILEGE)
        {
            TOKEN_PRIVILEGES wTP;
            LUID wLUID;
            IntPtr wProcessToken;
            LUID_AND_ATTRIBUTES[] wLUIDs = new LUID_AND_ATTRIBUTES[1];

            if (!OpenProcessToken(GetCurrentProcess(), TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, out wProcessToken))
                return;

            if (!LookupPrivilegeValue(null, SE_PRIVILEGE, out wLUID))
                return;

            wTP.PrivilegeCount = 1;
            wLUIDs[0].Luid = wLUID;
            wLUIDs[0].Attributes = SE_PRIVILEGE_ENABLED;
            wTP.Privileges = wLUIDs;

            if (!AdjustTokenPrivileges(wProcessToken, false, ref wTP, (uint)Marshal.SizeOf(wTP),
                IntPtr.Zero, IntPtr.Zero))
                return;
        }
        public static void SE_PRIVILEGE_DISABLE(string SE_PRIVILEGE)
        {
            TOKEN_PRIVILEGES wTP;
            LUID wLUID;
            IntPtr wProcessToken;
            LUID_AND_ATTRIBUTES[] wLUIDs = new LUID_AND_ATTRIBUTES[1];

            if (!OpenProcessToken(GetCurrentProcess(), TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, out wProcessToken))
                return;

            if (!LookupPrivilegeValue(null, SE_PRIVILEGE, out wLUID))
                return;

            wTP.PrivilegeCount = 1;
            wLUIDs[0].Luid = wLUID;
            wLUIDs[0].Attributes = 0;
            wTP.Privileges = wLUIDs;

            if (!AdjustTokenPrivileges(wProcessToken, false, ref wTP, (uint)Marshal.SizeOf(wTP),
                IntPtr.Zero, IntPtr.Zero))
                return;
        }
        #endregion Privilege
        #region Imports
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr CreateToolhelp32Snapshot(SnapshotFlags dwFlags, uint th32ProcessID);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern bool Module32First(IntPtr hSnapshot, ref MODULEENTRY32 lpme);
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern bool Module32Next(IntPtr hSnapshot, ref MODULEENTRY32 lpme);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern uint GetSystemDirectory([Out] StringBuilder lpBuffer, uint uSize);
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern uint GetWindowsDirectory([Out] StringBuilder lpBuffer, uint uSize);

        [DllImport("advapi32.dll", EntryPoint = "OpenSCManagerW", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr OpenSCManager(string machineName, string databaseName, uint dwAccess);
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr OpenService(IntPtr hSCManager, string lpServiceName, uint dwDesiredAccess);
        [DllImport("advapi32.dll", EntryPoint = "QueryServiceStatus", CharSet = CharSet.Unicode)]
        private static extern bool QueryServiceStatus(IntPtr hService, ref SERVICE_STATUS dwServiceStatus);
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseServiceHandle(IntPtr hSCObject);

        delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumChildWindows(IntPtr hWndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowVisible(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern unsafe IntPtr CreateFile(string FileName, uint DesiredAccess, uint ShareMode, uint SecurityAttributes, uint CreationDisposition, uint FlagsAndAttributes, uint hTemplateFile);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern unsafe bool ReadFile(IntPtr hFile, void* pBuffer, int NumberOfBytesToRead, int* pNumberOfBytesRead, System.Threading.NativeOverlapped* Overlapped);
        public static IntPtr ZCreateFile(string FileName, uint DesiredAccess, uint ShareMode, uint SecurityAttributes, uint CreationDisposition, uint FlagsAndAttributes, uint hTemplateFile)
        {
            return CreateFile(FileName, DesiredAccess, ShareMode, SecurityAttributes, CreationDisposition, FlagsAndAttributes, hTemplateFile);
        }
        public static unsafe bool ZReadFile(IntPtr hFile, void* pBuffer, int NumberOfBytesToRead, int* pNumberOfBytesRead, System.Threading.NativeOverlapped* Overlapped)
        {
            return ReadFile(hFile, pBuffer, NumberOfBytesToRead, pNumberOfBytesRead, Overlapped);
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetSystemMetrics(int nIndex);
        public static int ZGetSystemMetrics(int nIndex)
        {
            return GetSystemMetrics(nIndex);
        }
        
        [DllImport("shell32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);

        [DllImport("wintrust.dll", ExactSpelling = true, SetLastError = false, CharSet = CharSet.Unicode)]
        public static extern WinVerifyTrustResult WinVerifyTrust(IntPtr hwnd, [MarshalAs(UnmanagedType.LPStruct)] Guid pgActionID,
            WinTrustData pWVTData);
        #endregion Imports
        #region Enums
        [Flags]
        private enum SnapshotFlags : uint
        {
            HeapList = 0x00000001,
            Process = 0x00000002,
            Thread = 0x00000004,
            Module = 0x00000008,
            Module32 = 0x00000010,
            Inherit = 0x80000000,
            All = 0x0000001F,
            NoHeaps = 0x40000000
        }
        [Flags]
        public enum SCM_ACCESS : uint
        {
            /// <summary>
            /// Required to connect to the service control manager.
            /// </summary>
            SC_MANAGER_CONNECT = 0x00001,

            /// <summary>
            /// Required to call the CreateService function to create a service
            /// object and add it to the database.
            /// </summary>
            SC_MANAGER_CREATE_SERVICE = 0x00002,

            /// <summary>
            /// Required to call the EnumServicesStatusEx function to list the 
            /// services that are in the database.
            /// </summary>
            SC_MANAGER_ENUMERATE_SERVICE = 0x00004,

            /// <summary>
            /// Required to call the LockServiceDatabase function to acquire a 
            /// lock on the database.
            /// </summary>
            SC_MANAGER_LOCK = 0x00008,

            /// <summary>
            /// Required to call the QueryServiceLockStatus function to retrieve 
            /// the lock status information for the database.
            /// </summary>
            SC_MANAGER_QUERY_LOCK_STATUS = 0x00010,

            /// <summary>
            /// Required to call the NotifyBootConfigStatus function.
            /// </summary>
            SC_MANAGER_MODIFY_BOOT_CONFIG = 0x00020,

            /// <summary>
            /// Includes STANDARD_RIGHTS_REQUIRED, in addition to all access 
            /// rights in this table.
            /// </summary>
            SC_MANAGER_ALL_ACCESS = ACCESS_MASK.STANDARD_RIGHTS_REQUIRED |
                SC_MANAGER_CONNECT |
                SC_MANAGER_CREATE_SERVICE |
                SC_MANAGER_ENUMERATE_SERVICE |
                SC_MANAGER_LOCK |
                SC_MANAGER_QUERY_LOCK_STATUS |
                SC_MANAGER_MODIFY_BOOT_CONFIG,

            GENERIC_READ = ACCESS_MASK.STANDARD_RIGHTS_READ |
                SC_MANAGER_ENUMERATE_SERVICE |
                SC_MANAGER_QUERY_LOCK_STATUS,

            GENERIC_WRITE = ACCESS_MASK.STANDARD_RIGHTS_WRITE |
                SC_MANAGER_CREATE_SERVICE |
                SC_MANAGER_MODIFY_BOOT_CONFIG,

            GENERIC_EXECUTE = ACCESS_MASK.STANDARD_RIGHTS_EXECUTE |
                SC_MANAGER_CONNECT | SC_MANAGER_LOCK,

            GENERIC_ALL = SC_MANAGER_ALL_ACCESS,
        }
        public enum SERVICE_STATE : int
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }
        [Flags]
        public enum SERVICE_TYPE : int
        {
            SERVICE_KERNEL_DRIVER = 0x00000001,
            SERVICE_FILE_SYSTEM_DRIVER = 0x00000002,
            SERVICE_WIN32_OWN_PROCESS = 0x00000010,
            SERVICE_WIN32_SHARE_PROCESS = 0x00000020,
            SERVICE_INTERACTIVE_PROCESS = 0x00000100
        }
        [Flags]
        public enum ACCESS_MASK : uint
        {
            DELETE = 0x00010000,
            READ_CONTROL = 0x00020000,
            WRITE_DAC = 0x00040000,
            WRITE_OWNER = 0x00080000,
            SYNCHRONIZE = 0x00100000,

            STANDARD_RIGHTS_REQUIRED = 0x000F0000,

            STANDARD_RIGHTS_READ = 0x00020000,
            STANDARD_RIGHTS_WRITE = 0x00020000,
            STANDARD_RIGHTS_EXECUTE = 0x00020000,

            STANDARD_RIGHTS_ALL = 0x001F0000,

            SPECIFIC_RIGHTS_ALL = 0x0000FFFF,

            ACCESS_SYSTEM_SECURITY = 0x01000000,

            MAXIMUM_ALLOWED = 0x02000000,

            GENERIC_READ = 0x80000000,
            GENERIC_WRITE = 0x40000000,
            GENERIC_EXECUTE = 0x20000000,
            GENERIC_ALL = 0x10000000,

            DESKTOP_READOBJECTS = 0x00000001,
            DESKTOP_CREATEWINDOW = 0x00000002,
            DESKTOP_CREATEMENU = 0x00000004,
            DESKTOP_HOOKCONTROL = 0x00000008,
            DESKTOP_JOURNALRECORD = 0x00000010,
            DESKTOP_JOURNALPLAYBACK = 0x00000020,
            DESKTOP_ENUMERATE = 0x00000040,
            DESKTOP_WRITEOBJECTS = 0x00000080,
            DESKTOP_SWITCHDESKTOP = 0x00000100,

            WINSTA_ENUMDESKTOPS = 0x00000001,
            WINSTA_READATTRIBUTES = 0x00000002,
            WINSTA_ACCESSCLIPBOARD = 0x00000004,
            WINSTA_CREATEDESKTOP = 0x00000008,
            WINSTA_WRITEATTRIBUTES = 0x00000010,
            WINSTA_ACCESSGLOBALATOMS = 0x00000020,
            WINSTA_EXITWINDOWS = 0x00000040,
            WINSTA_ENUMERATE = 0x00000100,
            WINSTA_READSCREEN = 0x00000200,

            WINSTA_ALL_ACCESS = 0x0000037F
        }
        [Flags()]
        public enum SLR_FLAGS
        {
            SLR_NO_UI = 0x1,
            SLR_ANY_MATCH = 0x2,
            SLR_UPDATE = 0x4,
            SLR_NOUPDATE = 0x8,
            SLR_NOSEARCH = 0x10,
            SLR_NOTRACK = 0x20,
            SLR_NOLINKINFO = 0x40,
            SLR_INVOKE_MSI = 0x80
        }
        [Flags()]
        public enum SLGP_FLAGS
        {
            SLGP_SHORTPATH = 0x1,
            SLGP_UNCPRIORITY = 0x2,
            SLGP_RAWPATH = 0x4
        }
        public enum WinTrustDataUIChoice : uint
        {
            All = 1,
            None = 2,
            NoBad = 3,
            NoGood = 4
        }
        public enum WinTrustDataRevocationChecks : uint
        {
            None = 0x00000000,
            WholeChain = 0x00000001
        }
        public enum WinTrustDataChoice : uint
        {
            File = 1,
            Catalog = 2,
            Blob = 3,
            Signer = 4,
            Certificate = 5
        }
        public enum WinTrustDataStateAction : uint
        {
            Ignore = 0x00000000,
            Verify = 0x00000001,
            Close = 0x00000002,
            AutoCache = 0x00000003,
            AutoCacheFlush = 0x00000004
        }
        [Flags()]
        public enum WinTrustDataProvFlags : uint
        {
            UseIe4TrustFlag = 0x00000001,
            NoIe4ChainFlag = 0x00000002,
            NoPolicyUsageFlag = 0x00000004,
            RevocationCheckNone = 0x00000010,
            RevocationCheckEndCert = 0x00000020,
            RevocationCheckChain = 0x00000040,
            RevocationCheckChainExcludeRoot = 0x00000080,
            SaferFlag = 0x00000100,
            HashOnlyFlag = 0x00000200,
            UseDefaultOsverCheck = 0x00000400,
            LifetimeSigningFlag = 0x00000800,
            CacheOnlyUrlRetrieval = 0x00001000
        }
        public enum WinTrustDataUIContext : uint
        {
            Execute = 0,
            Install = 1
        }
        public enum WinVerifyTrustResult : uint
        {
            Success = 0,
            /// <summary>
            /// A system-level error occurred while verifying trust. 
            /// </summary>
            TRUST_E_SYSTEM_ERROR = 0x80096001,
            /// <summary>
            /// The certificate for the signer of the message is invalid or not found. 
            /// </summary>
            TRUST_E_NO_SIGNER_CERT = 0x80096002,
            /// <summary>
            /// One of the counter signatures was invalid. 
            /// </summary>
            TRUST_E_COUNTER_SIGNER = 0x80096003,
            /// <summary>
            /// The signature of the certificate cannot be verified. 
            /// </summary>
            TRUST_E_CERT_SIGNATURE = 0x80096004,
            /// <summary>
            /// The timestamp signature and/or certificate could not be verified or is malformed. 
            /// </summary>
            TRUST_E_TIME_STAMP = 0x80096005,
            /// <summary>
            /// The digital signature of the object did not verify. 
            /// </summary>
            TRUST_E_BAD_DIGEST = 0x80096010,
            /// <summary>
            /// A certificate's basic constraint extension has not been observed. 
            /// </summary>
            TRUST_E_BASIC_CONSTRAINTS = 0x80096019,
            /// <summary>
            /// The certificate does not meet or contain the Authenticode(tm) financial extensions. 
            /// </summary>
            TRUST_E_FINANCIAL_CRITERIA = 0x8009601E,
            /// <summary>
            /// Unknown trust provider. 
            /// </summary>
            TRUST_E_PROVIDER_UNKNOWN = 0x800B0001,
            /// <summary>
            /// The trust verification action specified is not supported by the specified trust provider. 
            /// </summary>
            TRUST_E_ACTION_UNKNOWN = 0x800B0002,
            /// <summary>
            /// The form specified for the subject is not one supported or known by the specified trust provider. 
            /// </summary>
            TRUST_E_SUBJECT_FORM_UNKNOWN = 0x800B0003,
            /// <summary>
            /// The subject is not trusted for the specified action. 
            /// </summary>
            TRUST_E_SUBJECT_NOT_TRUSTED = 0x800B0004,
            /// <summary>
            /// No signature was present in the subject. 
            /// </summary>
            TRUST_E_NOSIGNATURE = 0x800B0100,
            /// <summary>
            /// A required certificate is not within its validity period when verifying against the current system clock or the timestamp in the signed file. 
            /// </summary>
            CERT_E_EXPIRED = 0x800B0101,
            /// <summary>
            /// The validity periods of the certification chain do not nest correctly. 
            /// </summary>
            CERT_E_VALIDITYPERIODNESTING = 0x800B0102,
            /// <summary>
            /// A certificate that can only be used as an end-entity is being used as a CA or visa versa. 
            /// </summary>
            CERT_E_ROLE = 0x800B0103,
            /// <summary>
            /// A path length constraint in the certification chain has been violated. 
            /// </summary>
            CERT_E_PATHLENCONST = 0x800B0104,
            /// <summary>
            /// A certificate contains an unknown extension that is marked 'critical'. 
            /// </summary>
            CERT_E_CRITICAL = 0x800B0105,
            /// <summary>
            /// A certificate being used for a purpose other than the ones specified by its CA. 
            /// </summary>
            CERT_E_PURPOSE = 0x800B0106,
            /// <summary>
            /// A parent of a given certificate in fact did not issue that child certificate. 
            /// </summary>
            CERT_E_ISSUERCHAINING = 0x800B0107,
            /// <summary>
            /// A certificate is missing or has an empty value for an important field, such as a subject or issuer name. 
            /// </summary>
            CERT_E_MALFORMED = 0x800B0108,
            /// <summary>
            /// A certificate chain processed, but terminated in a root certificate which is not trusted by the trust provider. 
            /// </summary>
            CERT_E_UNTRUSTEDROOT = 0x800B0109,
            /// <summary>
            /// A certificate chain could not be built to a trusted root authority. 
            /// </summary>
            CERT_E_CHAINING = 0x800B010A,
            /// <summary>
            /// Generic trust failure. 
            /// </summary>
            TRUST_E_FAIL = 0x800B010B,
            /// <summary>
            /// A certificate was explicitly revoked by its issuer. 
            /// </summary>
            CERT_E_REVOKED = 0x800B010C,
            /// <summary>
            /// The certification path terminates with the test root which is not trusted with the current policy settings. 
            /// </summary>
            CERT_E_UNTRUSTEDTESTROOT = 0x800B010D,
            /// <summary>
            /// The revocation process could not continue - the certificate(s) could not be checked. 
            /// </summary>
            CERT_E_REVOCATION_FAILURE = 0x800B010E,
            /// <summary>
            /// The certificate's CN name does not match the passed value. 
            /// </summary>
            CERT_E_CN_NO_MATCH = 0x800B010F,
            /// <summary>
            /// The certificate is not valid for the requested usage. 
            /// </summary>
            CERT_E_WRONG_USAGE = 0x800B0110,
            /// <summary>
            /// The certificate was explicitly marked as untrusted by the user. 
            /// </summary>
            TRUST_E_EXPLICIT_DISTRUST = 0x800B0111,
            /// <summary>
            /// A certification chain processed correctly, but one of the CA certificates is not trusted by the policy provider. 
            /// </summary>
            CERT_E_UNTRUSTEDCA = 0x800B0112,
            /// <summary>
            /// The certificate has invalid policy. 
            /// </summary>
            CERT_E_INVALID_POLICY = 0x800B0113,
            /// <summary>
            /// The certificate has an invalid name. The name is not included in the permitted list or is explicitly excluded. 
            /// </summary>
            CERT_E_INVALID_NAME = 0x800B0114
        }
        #endregion Enums
        #region Structs
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct MODULEENTRY32
        {
            internal uint dwSize;
            internal uint th32ModuleID;
            internal uint th32ProcessID;
            internal uint GlblcntUsage;
            internal uint ProccntUsage;
            internal IntPtr modBaseAddr;
            internal uint modBaseSize;
            internal IntPtr hModule;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            internal string szModule;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            internal string szExePath;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SERVICE_STATUS
        {
            public static readonly int SizeOf = Marshal.SizeOf(typeof(SERVICE_STATUS));
            public SERVICE_TYPE dwServiceType;
            public SERVICE_STATE dwCurrentState;
            public uint dwControlsAccepted;
            public uint dwWin32ExitCode;
            public uint dwServiceSpecificExitCode;
            public uint dwCheckPoint;
            public uint dwWaitHint;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct WIN32_FIND_DATAW
        {
            public uint dwFileAttributes;
            public long ftCreationTime;
            public long ftLastAccessTime;
            public long ftLastWriteTime;
            public uint nFileSizeHigh;
            public uint nFileSizeLow;
            public uint dwReserved0;
            public uint dwReserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string cFileName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cAlternateFileName;
        }
        #endregion Structs
        #region Consts
        public const int MAX_PATH = 260;
        public const int SM_CLEANBOOT = 67;

        public const uint GENERIC_READ = 0x80000000;
        public const uint OPEN_EXISTING = 3;
        public const uint STGM_READ = 0;

        public static IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
        public static IntPtr INVALID_HANDLE = new IntPtr(0);
        #endregion Consts
        #region Interfaces
        [ComImport(), Guid("00021401-0000-0000-C000-000000000046")]
        public class ShellLink { }
        [ComImport(), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("000214F9-0000-0000-C000-000000000046")]
        public interface IShellLinkW
        {
            void GetPath([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, out WIN32_FIND_DATAW pfd, SLGP_FLAGS fFlags);
            void GetIDList(out IntPtr ppidl);
            void SetIDList(IntPtr pidl);
            void GetDescription([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);
            void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
            void GetWorkingDirectory([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);
            void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
            void GetArguments([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);
            void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
            void GetHotkey(out short pwHotkey);
            void SetHotkey(short wHotkey);
            void GetShowCmd(out int piShowCmd);
            void SetShowCmd(int iShowCmd);
            void GetIconLocation([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);
            void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
            void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);
            void Resolve(IntPtr hwnd, SLR_FLAGS fFlags);
            void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
        }
        [ComImport, Guid("0000010c-0000-0000-c000-000000000046"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPersist
        {
            [PreserveSig]
            void GetClassID(out Guid pClassID);
        }
        [ComImport, Guid("0000010b-0000-0000-C000-000000000046"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPersistFile : IPersist
        {
            new void GetClassID(out Guid pClassID);
            [PreserveSig]
            int IsDirty();
            [PreserveSig]
            void Load([In, MarshalAs(UnmanagedType.LPWStr)] string pszFileName, uint dwMode);
            [PreserveSig]
            void Save([In, MarshalAs(UnmanagedType.LPWStr)] string pszFileName,
                [In, MarshalAs(UnmanagedType.Bool)] bool fRemember);
            [PreserveSig]
            void SaveCompleted([In, MarshalAs(UnmanagedType.LPWStr)] string pszFileName);
            [PreserveSig]
            void GetCurFile([In, MarshalAs(UnmanagedType.LPWStr)] string ppszFileName);
        }
        #endregion Interfaces
        #region Functions
        [Flags]
        public enum ImportedDLLs : uint
        {
            NONE = 0x0,
            PSAPI = 0x1,
            RASAPI = 0x2,
            WinSock = 0x3,
            WinInet = 0x4
        }
        public static ImportedDLLs GetImpDlls(uint PID)
        {
            ImportedDLLs wResult = ImportedDLLs.NONE;
            IntPtr hSnapshot = CreateToolhelp32Snapshot(SnapshotFlags.Module, PID);
            MODULEENTRY32 wModuleEntry = new MODULEENTRY32();

            wModuleEntry.dwSize = (uint)Marshal.SizeOf(wModuleEntry);
            if (Module32First(hSnapshot, ref wModuleEntry))
                while (Module32Next(hSnapshot, ref wModuleEntry))
                {
                    if (wModuleEntry.szExePath.ToLower() == (GetSystem32Path() + "rasapi32.dll").ToLower())
                        wResult = wResult | ImportedDLLs.RASAPI;
                    else if (wModuleEntry.szExePath.ToLower() == (GetSystem32Path() + "wsock32.dll").ToLower())
                        wResult = wResult | ImportedDLLs.WinSock;
                    else if (wModuleEntry.szExePath.ToLower() == (GetSystem32Path() + "psapi.dll").ToLower())
                        wResult = wResult | ImportedDLLs.PSAPI;
                    else if (wModuleEntry.szExePath.ToLower() == (GetSystem32Path() + "wininet.dll").ToLower())
                        wResult = wResult | ImportedDLLs.WinInet;
                }

            return wResult;
        }

        public static string GetSystem32Path()
        {
            StringBuilder wPath = new StringBuilder(MAX_PATH);
            GetSystemDirectory(wPath, MAX_PATH);
            return wPath.ToString();
        }
        public static string GetWindowsPath()
        {
            StringBuilder wPath = new StringBuilder(MAX_PATH);
            GetWindowsDirectory(wPath, MAX_PATH);
            return wPath.ToString();
        }

        public static bool IsServiceStarted(string pSvcName)
        {
            IntPtr hSCMgr = IntPtr.Zero, hSvc = IntPtr.Zero;
            SERVICE_STATUS wSvc = new SERVICE_STATUS();
            hSCMgr = OpenSCManager(null, null, (uint)SCM_ACCESS.GENERIC_READ);
            if (hSCMgr != null && hSCMgr != INVALID_HANDLE_VALUE)
            {
                hSvc = OpenService(hSCMgr, pSvcName, (uint)ACCESS_MASK.GENERIC_READ);
                if (hSvc != null && hSvc != INVALID_HANDLE_VALUE)
                {
                    if (QueryServiceStatus(hSvc, ref wSvc))
                        if (wSvc.dwCurrentState == SERVICE_STATE.SERVICE_RUNNING)
                            return true;
                        else if (wSvc.dwCurrentState == SERVICE_STATE.SERVICE_STOPPED)
                            return false;
                }
            }
            CloseServiceHandle(hSCMgr);
            CloseServiceHandle(hSvc);
            return false;
        }

        public static bool Is64Bit()
        {
            return Marshal.SizeOf(typeof(IntPtr)) == 8;
        }

        public static bool HasVisibleWindows(int pPID)
        {
            IntPtr wHWND = System.Diagnostics.Process.GetProcessById(pPID).MainWindowHandle;
            if (wHWND == INVALID_HANDLE)
                return false;

            IntPtr wHWNDParent = GetParent(wHWND);
            if (wHWNDParent == INVALID_HANDLE)
                wHWNDParent = wHWND;

            bool wResult = false;
            EnumChildWindows(wHWNDParent,
                new EnumWindowsProc((hWnd, lParam) =>
                {
                    if (GetParent(hWnd) != wHWNDParent)
                        return false;
                    if (IsWindowVisible(hWnd))
                        wResult = true;
                    return true;
                }), IntPtr.Zero);
            return wResult;
        }

        public static string ResolveShortcut(string pFileName)
        {
            ShellLink wLink = new ShellLink();
            ((IPersistFile)wLink).Load(pFileName, STGM_READ);
            StringBuilder wPath = new StringBuilder(MAX_PATH);
            WIN32_FIND_DATAW wData = new WIN32_FIND_DATAW();
            ((IShellLinkW)wLink).GetPath(wPath, wPath.Capacity, out wData, 0);
            return wPath.ToString();
        }

        public static string ShortToLongFilename(string pShortFilename)
        {
            return new System.IO.FileInfo(pShortFilename).FullName;
        }
        #endregion Functions
    }
}