/*
 * WinChecker 0.3.spata-alpha
 * Authors: ZAccess, adm1nn
 * (c) 2016
 */
using System;

using Microsoft.Win32;

namespace WinChecker.API
{
    static class WinReg
    {
        public const char cSlash = '\\';
        ///////////////////////////////////////////////////////////////////////
        #region Consts
        public const string HKLM_Run = @"HKLM\Software\Microsoft\Windows\CurrentVersion\Run";
        public const string HKLM_Run64 = @"HKLM\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Run";
        public const string HKLM_RunOnce = @"HKLM\Software\Microsoft\Windows\CurrentVersion\RunOnce";
        public const string HKLM_RunOnce64 = @"HKLM\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\RunOnce";
        public const string HKLM_RunOnceEx = @"HKLM\Software\Microsoft\Windows\CurrentVersion\RunOnceEx";
        public const string HKLM_RunSvcs = @"HKLM\Software\Microsoft\Windows\CurrentVersion\RunServices";
        public const string HKLM_RunSvcsOnce = @"HKLM\Software\Microsoft\Windows\CurrentVersion\RunServicesOnce";
        public const string HKLM_Policies_Explorer_Run = @"HKLM\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\Run";
        public const string HKLM_TS_Run = @"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\Install\Software\Microsoft\Windows\CurrentVersion\Run";
        public const string HKLM_TS_RunOnce = @"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\Install\Software\Microsoft\Windows\CurrentVersion\RunOnce";
        public const string HKLM_TS_RunOnceEx = @"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\Install\Software\Microsoft\Windows\CurrentVersion\RunOnceEx";
        public const string HKLM_Policies_Explorer = @"HKLM\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer";
        public const string HKLM_Explorer_DisallowRun = @"HKLM\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun";
        public const string HKLM_Explorer_RestrictRun = @"HKLM\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\RestrictRun";
        public const string HKLM_Policies_System = @"HKLM\Software\Microsoft\Windows\CurrentVersion\Policies\System";
        public const string HKLM_CCS_SM = @"HKLM\SYSTEM\CurrentControlSet\Control\Session Manager";
        public const string HKLM_CCS_PM = @"HKLM\SYSTEM\CurrentControlSet\Control\Print\Monitors";
        public const string HKLM_CCS_KDLLs = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\KnownDLLs";
        public const string HKLM_CCS_LSA = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Lsa";
        public const string HKLM_CCS_SP = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\SecurityProviders";
        public const string HKLM_CCS_NP = @"HKLM\SYSTEM\CurrentControlSet\Services\RDPNP\NetworkProvider";
        public const string HKLM_CCS_NP2 = @"HKLM\SYSTEM\CurrentControlSet\Services\LanmanWorkstation\NetworkProvider";
        public const string HKLM_CCS_NP3 = @"HKLM\SYSTEM\CurrentControlSet\Services\webclient\NetworkProvider";
        public const string HKLM_CCS_WSP = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WinSock2\Parameters\Protocol_Catalog9\Catalog_Entries";
        public const string HKLM_CCS_WSP2 = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WinSock2\Parameters\NameSpace_Catalog5\Catalog_Entries";
        public const string HKLM_CCS_TCPIP_Params = @"HKLM\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters";
        public const string HKLM_CCS_TCPIP_PR = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\PersistentRoutes";
        public const string HKLM_IE = @"HKLM\Software\Microsoft\Internet Explorer";
        public const string HKLM_IE_Main = @"HKLM\Software\Microsoft\Internet Explorer\Main";
        public const string HKLM_IE_Toolbars = @"HKLM\Software\Microsoft\Internet Explorer\Toolbar";
        public const string HKLM_IE_Exts = @"HKLM\Software\Microsoft\Internet Explorer\Extensions";
        public const string HKLM_IE_AS = @"HKLM\SOFTWARE\Microsoft\Internet Explorer\AdvancedOptions";
        public const string HKLM_FF = @"HKLM\Software\Mozilla\Mozilla Firefox";
        public const string HKLM_FF2 = @"HKLM\Software\mozilla.org\Mozilla";
        public const string HKLM_FF_Plugins = @"HKLM\Software\MozillaPlugins";
        public const string HKLM_CH_Ver = @"HKCU\Software\Google\Chrome\BLBeacon";
        public const string HKLM_CH_Ver2 = @"HKLM\Software\Google\Update\Clients\{8A69D345-D564-463c-AFF1-A69D9E530F96}";
        public const string HKLM_CH_Exts = @"HKLM\Software\Google\Chrome\Extensions";
        public const string HKLM_YA_Exts = @"HKLM\Software\Yandex\Extensions";
        public const string HKLM_EX_Filters = @"HKLM\Software\Classes\Protocols\Filter";
        public const string HKLM_EX_Handlers = @"HKLM\Software\Classes\Protocols\Handler";
        public const string HKLM_EX_DC = @"HKCU\Software\Microsoft\Internet Explorer\Desktop\Components";
        public const string HKLM_EX_SEH = @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\ShellExecuteHooks";
        public const string HKLM_Windows = @"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows";
        public const string HKLM_Windows64 = @"HKLM\SOFTWARE\Wow6432Node\Microsoft\Windows NT\CurrentVersion\Windows";
        public const string HKLM_SSODL = @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\ShellServiceObjectDelayLoad";
        public const string HKLM_Winlogon = @"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon";
        public const string HKLM_Winlogon_Notify = @"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon\Notify";
        public const string HKLM_ShellFolders = @"HKLM\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders";
        public const string HKCU_Run = @"HKCU\Software\Microsoft\Windows\CurrentVersion\Run";
        public const string HKCU_RunOnce = @"HKCU\Software\Microsoft\Windows\CurrentVersion\RunOnce";
        public const string HKCU_Policies_Explorer_Run = @"HKCU\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\Run";
        public const string HKCU_RunSvcs = @"HKCU\Software\Microsoft\Windows\CurrentVersion\RunServices";
        public const string HKCU_TS_Run = @"HKCU\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\Install\Software\Microsoft\Windows\CurrentVersion\Run";
        public const string HKCU_TS_RunOnce = @"HKCU\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\Install\Software\Microsoft\Windows\CurrentVersion\RunOnce";
        public const string HKCU_TS_RunOnceEx = @"HKCU\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\Install\Software\Microsoft\Windows\CurrentVersion\RunOnceEx";
        public const string HKCU_Policies_Explorer = @"HKCU\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer";
        public const string HKCU_Explorer_DisallowRun = @"HKCU\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun";
        public const string HKCU_Explorer_RestrictRun = @"HKCU\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\RestrictRun";
        public const string HKCU_Policies_System = @"HKCU\Software\Microsoft\Windows\CurrentVersion\Policies\System";
        public const string HKCU_Policies_IE_Restrictions = @"HKCU\Software\Policies\Microsoft\Internet Explorer\Restrictions";
        public const string HKCU_IE_URLHooks = @"HKCU\Software\Microsoft\Internet Explorer\URLSearchHooks";
        public const string HKCU_IE_MenuExts = @"HKCU\Software\Microsoft\Internet Explorer\MenuExt";
        public const string HKCU_IE_Settings = @"HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings";
        public const string HKCU_FF_Plugins = @"HKCU\Software\MozillaPlugins";
        public const string HKCU_YA_Ver = @"HKCU\Software\Yandex\BLBeacon";
        public const string HKCU_SSODL = @"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\ShellServiceObjectDelayLoad";
        public const string HKCU_ShellFolders = @"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders";
        #endregion Consts
        ///////////////////////////////////////////////////////////////////////
        static RegistryKey wKey = null;

        public static bool OpenKey(string pKey, out RegistryKey rKey)
        {
            rKey = null;
            try
            {
                switch (pKey.Split(cSlash)[0])
                {
                    case "HKLM":
                        rKey = Registry.LocalMachine.OpenSubKey(pKey.Substring(5), true);
                        break;
                    case "HKCU":
                        rKey = Registry.CurrentUser.OpenSubKey(pKey.Substring(5), true);
                        break;
                    case "HKCR":
                        rKey = Registry.ClassesRoot.OpenSubKey(pKey.Substring(5), true);
                        break;
                    case "HKPD":
                        rKey = Registry.PerformanceData.OpenSubKey(pKey.Substring(5), true);
                        break;
                    case "HKCC":
                        rKey = Registry.CurrentConfig.OpenSubKey(pKey.Substring(5), true);
                        break;
                    case "HKDD":
                        rKey = Registry.DynData.OpenSubKey(pKey.Substring(5), true);
                        break;
                    case "HKU":
                        rKey = Registry.Users.OpenSubKey(pKey.Substring(5), true);
                        break;
                }
            }
            catch
            {
                return false;
            }
            return rKey != null;
        }
        public static bool GetStringValue(string pKey, string pValueName, out string rString)
        {
            rString = null;
            object wValue = null;
            try
            {
                if (OpenKey(pKey, out wKey))
                    if ((wValue = wKey.GetValue(pValueName, null, RegistryValueOptions.DoNotExpandEnvironmentNames)) != null)
                        switch (wKey.GetValueKind(pValueName))
                        {
                            case RegistryValueKind.MultiString:
                                if (wValue != null)
                                    foreach (string wString in (string[])wValue)
                                        rString = wString + ", ";
                                break;
                            default:
                                if (wValue is byte[])
                                    return false;
                                rString = wValue.ToString();
                                break;
                        }
            }
            catch
            {
                return false;
            }
            wValue = null;
            return !string.IsNullOrEmpty(rString);
        }
        public static bool GetIntValue(string pKey, string pValueName, out int rInt)
        {
            rInt = -1;
            try
            {
                if (OpenKey(pKey, out wKey))
                    rInt = Convert.ToInt32(wKey.GetValue(pValueName, -1));
            }
            catch
            {
                return false;
            }
            return rInt != -1;
        }
        public static bool GetValueNames(string pKey, out string[] rNames)
        {
            rNames = null;
            try
            {
                if (OpenKey(pKey, out wKey))
                    rNames = wKey.GetValueNames();
            }
            catch
            {
                return false;
            }
            return rNames != null;
        }
        public static bool GetSubkeys(string pKey, out string[] rNames)
        {
            rNames = null;
            try
            {
                if (OpenKey(pKey, out wKey))
                    rNames = wKey.GetSubKeyNames();
            }
            catch
            {
                return false;
            }
            return rNames != null;
        }
        public static bool GetSubkeysInRootKey(RegistryKey pKey, out string[] rNames)
        {
            rNames = null;
            try
            {
                rNames = pKey.GetSubKeyNames();
            }
            catch
            {
                return false;
            }
            return rNames != null;
        }
        public static void CloseKey()
        {
            wKey.Close();
        }
    }
}