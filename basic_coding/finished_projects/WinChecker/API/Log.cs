/*
 * WinChecker 0.3.spata-alpha
 * Authors: ZAccess, adm1nn
 * (c) 2016
 */
using System;
using System.IO;
using System.Text;
using System.Management;
using System.Diagnostics;
using System.Security.Cryptography;

using static WinChecker.API.WinAPI;
using static WinChecker.API.WinReg;
using static WinChecker.API.Policies;

namespace WinChecker.API
{
    static class Log
    {
        static StreamWriter wLog;
        static bool NotNull(object wObj)
        {
            if (wObj != null)
                return !string.IsNullOrEmpty(wObj.ToString());
            else
                return false;
        }

        #region FileInfo
        static bool IsExecutableFile(string pFile)
        {
            return pFile.EndsWith(".exe") || pFile.EndsWith(".dll") || pFile.EndsWith(".pif") || pFile.EndsWith(".EXE")
                || pFile.EndsWith(".DLL") || pFile.EndsWith(".PIF");
        }

        static string GetFileMD5(string pPath)
        {
            MD5 wMD5Hasher = MD5.Create();
            byte[] wData = wMD5Hasher.ComputeHash(File.ReadAllBytes(pPath));
            StringBuilder wHash = new StringBuilder();
            for (int i = 0; i < wData.Length; i++)
                wHash.Append(wData[i].ToString("x2"));
            wData = null;
            wMD5Hasher.Clear();
            return wHash.ToString();
        }
        static string GetFullPath(string pText)
        {
            if (pText.Contains("\""))
            {
                char[] wResult = pText.ToCharArray();
                int wIndex = 0;

                for (wIndex = 0; wIndex < wResult.Length; wIndex++)
                    if (wResult[wIndex] == '"')
                        break;
                wIndex++;

                for (int i = wIndex + 1; i < wResult.Length; i++)
                    if (wResult[i] == '"')
                        return pText.Substring(wIndex, --i);
            }
            else if (pText.Contains("/"))
            {
                char[] wResult = pText.ToCharArray();
                int wIndex = 0;

                for (wIndex = 0; wIndex < wResult.Length; wIndex++)
                    if (wResult[wIndex] == '/')
                        break;

                return pText.Substring(0, --wIndex);
            }
            return pText;
        }
        static string GetFileAttributes(FileAttributes pFileAttribs)
        {
            return pFileAttribs.ToString().Replace("Archive", "Архивный").Replace("Compressed", "Сжатый")
                .Replace("Encrypted", "Зашифрованный").Replace("Hidden", "Скрытый").Replace("ReadOnly", "Только для чтения")
                .Replace("System", "Системный").Replace("Temporary", "Временный");
        }
        static void GetFileInfo(string pPath, bool pWriteText = true)
        {
            if (WinTrust.VerifyEmbeddedSignature(pPath))
                return;
            byte wSuspRate = 0;

            if (!File.Exists(pPath))
                wLog.WriteLine("    [!] Файл не найден", pPath);
            else
            {
                if (pWriteText)
                    wLog.WriteLine("Анализ файла: {0}", pPath);
                wLog.WriteLine("    MD5: {0}", GetFileMD5(pPath));

                try
                {
                    FileVersionInfo wFileVersionInfo = FileVersionInfo.GetVersionInfo(pPath);
                    FileInfo wFileInfo = new FileInfo(pPath);

                    if (NotNull(wFileVersionInfo.FileDescription))
                        wLog.WriteLine("    Описание: {0}", wFileVersionInfo.FileDescription);
                    else
                    {
                        wLog.WriteLine("    [-] Файл не имеет описания");
                        wSuspRate++;
                    }
                    if (NotNull(wFileVersionInfo.CompanyName))
                        wLog.WriteLine("    Организация: {0}", wFileVersionInfo.CompanyName);
                    else
                    {
                        wLog.WriteLine("    [-] Файл не имеет информации об организации");
                        wSuspRate++;
                    }
                    if (NotNull(wFileVersionInfo.FileVersion.ToString()))
                        wLog.WriteLine("    Версия: {0}", wFileVersionInfo.FileVersion.ToString());
                    else
                    {
                        wLog.WriteLine("    [-] Нет информации о версии файла");
                        wSuspRate++;
                    }
                    
                    wLog.WriteLine("    [-] Файл не имеет цифровой подписи");
                    wSuspRate++;

                    wLog.WriteLine("    Размер: {0}КБ", Math.Round((double)wFileInfo.Length / 1024, 2));
                    wLog.WriteLine("    Атрибуты: {0}", GetFileAttributes(wFileInfo.Attributes));
                    if (wFileInfo.Attributes.ToString().Contains("Hidden"))
                        wSuspRate++;
                    if (wFileInfo.Attributes.ToString().Contains("Hidden") && wFileInfo.Attributes.ToString().Contains("System"))
                        wSuspRate++;
                    wLog.WriteLine("    Время изменения: {0}", wFileInfo.LastWriteTime.ToString());
                    if (wSuspRate != 0)
                        wLog.WriteLine("    Рейтинг подозрительности: {0}/6", wSuspRate);
                    wFileVersionInfo = null;
                    wFileInfo = null;
                }
                catch
                {
                    wLog.WriteLine("    [!] Ошибка при получении информации о файле");
                    if (wSuspRate != 0)
                        wLog.WriteLine("    Рейтинг подозрительности: {0}/5", wSuspRate);
                }
            }
        }
        #endregion FileInfo

        static void ParseAutorunKey(string pKey, string pText)
        {
            string[] wBufferNames = null;
            string wBuffer = null;

            if (GetValueNames(pKey, out wBufferNames))
                for (int i = 0; i < wBufferNames.Length; i++)
                {
                    if (!GetStringValue(pKey, wBufferNames[i], out wBuffer))
                    {
                        wLog.WriteLine("{0} => {1} => ???", pText, wBufferNames[i]);
                        wLog.WriteLine("[!] Не удалось прочитать значение реестра");
                    }
                    else
                    {
                        wLog.WriteLine("{0} => {1} => {2}", pText, wBufferNames[i], wBuffer);
                        GetFileInfo(GetFullPath(wBuffer));
                    } // GetStringValue(pKey, wBufferNames[i], out wBuffer)
                }
            if (GetSubkeys(pKey, out wBufferNames))
                for (int i = 0; i < wBufferNames.Length; i++)
                    ParseAutorunKey(pKey + "\\" + wBufferNames[i], pText + "\\" + wBufferNames[i]);

            wBufferNames = null;
            wBuffer = null;
        }
        static void ParseAutorunValue(string pKey, string pValueName, string pText)
        {
            string wBuffer = null;
            if (GetStringValue(pKey, pValueName, out wBuffer))
            {
                wLog.WriteLine("{0} => {1} => {2}", pText, pValueName, wBuffer);
                GetFileInfo(GetFullPath(wBuffer));
            }
            wBuffer = null;
        }
        static void WriteAllValues(string pKey)
        {
            string[] wValueNames = null;
            string wBuffer = null;
            if (GetValueNames(pKey, out wValueNames))
                for (int i = 0; i < wValueNames.Length; i++)
                    wLog.WriteLine(wValueNames[i] + " => " + (GetStringValue(pKey, wValueNames[i], out wBuffer) ? wBuffer : ""));
            wValueNames = null;
            wBuffer = null;
        }

        static void GetExtensionInfo(string pExtension)
        {
            string wBuffer = null;
            if (GetStringValue("HKCR\\" + pExtension + "file", "NeverShowExt", out wBuffer))
                if (wBuffer == "")
                    wLog.WriteLine((GetStringValue("HKCR\\" + pExtension + "file", "IsShortcut", out wBuffer)
                        && wBuffer != "" && wBuffer.ToLower() != "yes") ?
                        "[!!] .{0} => Скрыто(Не отображается стрелка)" : "[!] .{0} => Скрыто", pExtension);
            wBuffer = null;
        }
        static void GetCLSIDInfo(string pCLSID)
        {
            string wPath;
            if (GetStringValue("HKCR\\CLSID\\" + pCLSID + "\\InprocServer32", "", out wPath) && NotNull(wPath))
            {
                wLog.WriteLine("    {0} => {1}", pCLSID, wPath);
                if (IsExecutableFile(wPath))
                    GetFileInfo(wPath, false);
            }
        }

        static string GetHostsFolder()
        {
            string wPath = null;
            GetStringValue(@"HKLM\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters", "DataBasePath", out wPath);
            return wPath;
        }
        unsafe static int FileRead(IntPtr hFile, byte[] lpBuffer, int nIndex, int nCount)
        {
            int n = 0;
            System.Threading.NativeOverlapped wNull;
            fixed (byte* p = lpBuffer)
                return ZReadFile(hFile, p + nIndex, nCount, &n, &wNull) ? 0 : n;
        }

        static void ParseAutorunFolder(string pKey, string pValueName)
        {
            string wPath = null;
            if (GetStringValue(pKey, pValueName, out wPath))
                if (Directory.Exists(wPath))
                {
                    string[] wPathes = Directory.GetFiles(wPath);
                    for (int i = 0; i < wPathes.Length; i++)
                        if (wPathes[i].EndsWith(".lnk"))
                        {
                            string wRealPath = ResolveShortcut(wPathes[i]);

                            if (NotNull(wRealPath))
                            {
                                wLog.WriteLine("[Ярлык] => {0}", wRealPath);
                                wLog.WriteLine("    MD5: {0}", GetFileMD5(wRealPath));
                                try
                                {
                                    FileInfo wFileInfo = new FileInfo(wRealPath);
                                    wLog.WriteLine("    Размер: {0}КБ", Math.Round((double)wFileInfo.Length / 1024, 2));
                                    wLog.WriteLine("    Атрибуты: {0}", wFileInfo.Attributes.ToString());
                                    wLog.WriteLine("    Время изменения: {0}", wFileInfo.LastWriteTime.ToString());
                                    wFileInfo = null;
                                }
                                catch
                                {
                                    wLog.WriteLine("    [!] Ошибка при получении информации о файле");
                                }
                            }
                            else
                                wLog.WriteLine("[!] Неизвестный ярлык: {0}", wPathes[i]);

                            wRealPath = null;
                        }
                        else
                        {
                            if (NotNull(wPathes[i]) && !wPathes[i].EndsWith("desktop.ini"))
                            {
                                wLog.WriteLine(wPathes[i]);
                                wLog.WriteLine("    MD5: {0}", wPathes[i]);
                                try
                                {
                                    FileInfo wFileInfo = new FileInfo(wPathes[i]);
                                    wLog.WriteLine("    Размер: {0}КБ", Math.Round((double)wFileInfo.Length / 1024, 2));
                                    wLog.WriteLine("    Атрибуты: {0}", wFileInfo.Attributes.ToString());
                                    wLog.WriteLine("    Время изменения: {0}", wFileInfo.LastWriteTime.ToString());
                                    wFileInfo = null;
                                }
                                catch
                                {
                                    wLog.WriteLine("    [!] Ошибка при получении информации о файле");
                                }
                            } // NotNull(wPathes[i])
                        } // wPathes[i].EndsWith(".lnk")
                    wPathes = null;
                }
            wPath = null;
        }

        public static void CreateLog()
        {
            string wStartupPath = System.Windows.Forms.Application.StartupPath;
            wLog = new StreamWriter(wStartupPath + "\\WinCheck.log");
            DateTime wStartTime = DateTime.UtcNow;

            wLog.AutoFlush = true;

            wLog.WriteLine("WinChecker " + Program.wVersion);
            wLog.WriteLine("Протокол исследования системы");

            #region AV
            try
            {
                ManagementObjectSearcher wAVInfo = new ManagementObjectSearcher("root\\SecurityCenter", "SELECT * From AntiVirusProduct");
                ManagementObjectCollection wAVs = wAVInfo.Get();

                if (wAVs.Count > 0)
                {
                    wLog.WriteLine("--------------------------------------------{0}--------------------------------------------", "Антивирусы");
                    foreach (ManagementObject wAV in wAVs)
                    {
                        wLog.WriteLine("Имя: {0}", wAV["DisplayName"]);
                        wLog.WriteLine("    Версия: {0}", NotNull(wAV["VersionNumber"]) ? wAV["VersionNumber"] : "???");
                        wLog.WriteLine("    Обновлен: {0}", NotNull(wAV["productUpToDate"]) ? wAV["productUpToDate"] : "???");
                    }
                }

                wAVInfo.Dispose();
                wAVs.Dispose();
            }
            catch { }
            #endregion
            #region Process
            wLog.WriteLine("--------------------------------------------{0}--------------------------------------------", "Процессы");
            try
            {
                SE_PRIVILEGE_ENABLE(SE_DEBUG_NAME);
                ManagementObjectSearcher wProcInfo = new ManagementObjectSearcher("root\\CIMV2",
                            "SELECT CommandLine, ExecutablePath, ProcessId FROM Win32_Process");
                ManagementObjectCollection wProcesses = wProcInfo.Get();

                foreach (ManagementObject wProcess in wProcesses)
                {
                    if (!NotNull(wProcess["ExecutablePath"]) && !NotNull(wProcess["CommandLine"]))
                        continue;
                    wLog.WriteLine("Процесс: {0}", wProcess["ExecutablePath"]);
                    wLog.WriteLine("    Командная строка: {0}", wProcess["CommandLine"]);
                    wLog.WriteLine("    PID: {0}", wProcess["ProcessId"]);

                    try
                    {
                        GetFileInfo((string)wProcess["ExecutablePath"], false);

                        if (!HasVisibleWindows((int)wProcess["ProcessId"]))
                            wLog.WriteLine("[!] Не имеет видимых окон");

                        string wAPIs = GetImpDlls((uint)wProcess["ProcessId"]).ToString();
                        if (wAPIs.Contains("PSAPI"))
                            wLog.WriteLine("[!] Может использовать PSAPI");
                        if (wAPIs.Contains("WinSock"))
                            wLog.WriteLine("[!] Может использовать WinSock");
                        if (wAPIs.Contains("RASAPI"))
                            wLog.WriteLine("[!] Может использовать RASAPI");
                        if (wAPIs.Contains("WinInet"))
                            wLog.WriteLine("[!] Может использовать Wininet");
                    }
                    catch { }
                }
                wProcesses.Dispose();
                wProcInfo.Dispose();
                SE_PRIVILEGE_DISABLE(SE_DEBUG_NAME);
            }
            catch { }
            #endregion Process
            #region Services
            wLog.WriteLine("--------------------------------------------{0}--------------------------------------------", "Сервисы");
            try
            {
                ManagementObjectSearcher wServices = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Service");
                int wServiceCount = 0, wWhiteListServiceCount = 0;

                foreach (ManagementObject queryObj in wServices.Get())
                {
                    if (NotNull(queryObj["PathName"]))
                        if (!CheckSvc.PathInWhiteList((string)queryObj["PathName"]))
                            if (NotNull(queryObj["Name"]))
                            {
                                wLog.WriteLine((bool.Parse(queryObj["Started"].ToString()) ? "(Включен) " : "(Выключен) ") +
                                    queryObj["Name"] + (NotNull(queryObj["PathName"]) ? " => [" + queryObj["PathName"] + "]" : ""));
                                GetFileInfo(GetFullPath((string)queryObj["PathName"]));
                            } //NotNull(queryObj["Name"])
                            else
                                continue;
                        else
                            wWhiteListServiceCount++;
                    wServiceCount++;
                }
                wLog.WriteLine("Всего сервисов: {0}", wServiceCount);
                wLog.WriteLine("Опознанно по базе безопасных: {0}", wWhiteListServiceCount);

                wServices.Dispose();
                wServiceCount = 0;
                wWhiteListServiceCount = 0;
            }
            catch { }
            #endregion Services
            #region Autorun
            wLog.WriteLine("--------------------------------------------{0}--------------------------------------------", "Автозапуск");
            try
            {
                ParseAutorunKey(HKLM_Run, "HKLM\\...\\Run");
                ParseAutorunKey(HKLM_Run64, "HKLM\\...\\Wow6432Node\\...\\Run");
                ParseAutorunKey(HKLM_RunOnce, "HKLM\\...\\RunOnce");
                ParseAutorunKey(HKLM_RunOnce64, "HKLM\\...\\Wow6432Node\\...\\RunOnce");
                ParseAutorunKey(HKLM_RunSvcs, "HKLM\\...\\RunServices");
                ParseAutorunKey(HKLM_RunOnce64, "HKLM\\...\\Wow6432Node\\...\\RunOnce");
                ParseAutorunKey(HKLM_RunSvcsOnce, "HKLM\\...\\RunServicesOnce");
                ParseAutorunKey(HKLM_Policies_Explorer_Run, "HKLM\\...\\Policies\\Explorer\\Run");
                ParseAutorunKey(HKLM_TS_Run, "HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Terminal Server\\...\\CurrentVersion\\Run");
                ParseAutorunKey(HKLM_TS_RunOnce, "HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Terminal Server\\...\\CurrentVersion\\RunOnce");
                ParseAutorunKey(HKLM_TS_RunOnceEx, "HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Terminal Server\\...\\CurrentVersion\\RunOnceEx");

                ParseAutorunValue(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows", "run", @"HKEY_LOCAL_MACHINE\...\Windows");
                ParseAutorunValue(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows", "load", @"HKEY_LOCAL_MACHINE\...\Windows");
                ParseAutorunValue(HKLM_Policies_Explorer, "shell", @"HKEY_LOCAL_MACHINE\...\Policies\Explorer");
                ParseAutorunValue(@"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", "shell", @"HKEY_LOCAL_MACHINE\...\Policies\System");
                ParseAutorunValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Terminal Server\Wds\rdpwd", "run", @"HKLM\SYSTEM\CCS\Control\Terminal Server\Wds\rdpwd");
                ParseAutorunValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Terminal Server\Wds\rdpwd", "StartupPrograms", @"HKLM\SYSTEM\CCS\Control\Terminal Server\Wds\rdpwd");
                ParseAutorunValue(HKLM_CCS_SM, "BootExecute", @"HKLM\SYSTEM\CCS\Control\Session Manager");
                ParseAutorunValue(HKLM_CCS_SM, "Execute", @"HKLM\SYSTEM\CCS\Control\Session Manager");
                ParseAutorunValue(HKLM_CCS_SM, "StartupExecute", @"HKLM\SYSTEM\CCS\Control\Session Manager");
                ParseAutorunValue(@"HKLM\SOFTWARE\Microsoft\Command Processor", "AutoRun", @"HKLM\SOFTWARE\Microsoft\Command Processor");

                ParseAutorunKey(HKCU_Run, "HKCU\\...\\Run");
                ParseAutorunKey(HKCU_RunOnce, "HKCU\\...\\RunOnce");
                ParseAutorunKey(HKCU_Policies_Explorer_Run, "HKCU\\...\\Policies\\Explorer\\Run");
                ParseAutorunKey(HKCU_RunSvcs, "HKCU\\...\\RunServices");
                ParseAutorunKey(HKCU_TS_Run, "HKCU\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Terminal Server\\...\\CurrentVersion\\Run");
                ParseAutorunKey(HKCU_TS_RunOnce, "HKCU\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Terminal Server\\...\\CurrentVersion\\RunOnce");
                ParseAutorunKey(HKCU_TS_RunOnceEx, "HKCU\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Terminal Server\\...\\CurrentVersion\\RunOnceEx");

                ParseAutorunValue(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows", "run", @"HKCU\...\Windows");
                ParseAutorunValue(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows", "load", @"HKCU\...\Windows");
                ParseAutorunValue(@"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer", "shell", @"HKEY_CURRENT_USER\...\Policies\Explorer");
                ParseAutorunValue(@"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", "shell", @"HKCU\...\Windows");
                ParseAutorunValue(@"HKCU\SOFTWARE\Microsoft\Command Processor", "AutoRun", @"HKCU\SOFTWARE\Microsoft\Command Processor");

                ParseAutorunFolder(HKCU_ShellFolders, "Startup");
                ParseAutorunFolder(HKCU_ShellFolders, "AltStartup");
                ParseAutorunFolder(HKLM_ShellFolders, "Startup");
                ParseAutorunFolder(HKLM_ShellFolders, "AltStartup");
                ParseAutorunFolder(HKLM_ShellFolders, "Global Startup");
            }
            catch { }
            #endregion Autorun
            #region PrintMonitors
            wLog.WriteLine("--------------------------------------------{0}--------------------------------------------", "Мониторы печати");
            try
            {
                string[] wMonitors = null;
                string wBuffer = null;
                if (GetSubkeys(HKLM_CCS_PM, out wMonitors))
                    foreach (string wMonitor in wMonitors)
                        if (NotNull(wMonitor))
                            if (GetStringValue(HKLM_CCS_PM + "\\" + wMonitor, "Driver", out wBuffer))
                            {
                                string wPath;

                                if (wBuffer.Split(cSlash).Length > 1)
                                    wPath = wBuffer;
                                else
                                    wPath = GetSystem32Path() + "\\" + wBuffer;

                                wLog.WriteLine(wMonitor);
                                if (!File.Exists(wPath))
                                    wLog.WriteLine("Файл не найден: {0}", wPath);
                                else
                                {
                                    wLog.WriteLine("Путь к файлу: {0}", wPath);
                                    GetFileInfo(wPath, false);
                                }

                                wPath = null;
                            }
                            else
                            {
                                wLog.WriteLine(wMonitor);
                                wLog.WriteLine("[!] Не удалось получить информацию из реестра");
                            }
                wBuffer = null;
                wMonitors = null;
            }
            catch { }
            #endregion
            #region Winlogon
            wLog.WriteLine("--------------------------------------------{0}--------------------------------------------", @"HKLM(HKCU)\...\Windows NT\Winlogon");
            try
            {
                string[] wBuffers = null;
                string wBuffer = null;
                if (GetStringValue(HKLM_Winlogon, "Userinit", out wBuffer))
                    wLog.WriteLine("Userinit: {0}", wBuffer);
                if (GetStringValue(HKLM_Winlogon, "Shell", out wBuffer))
                    wLog.WriteLine("Shell: {0}", wBuffer);
                if (GetStringValue(@"HKCU\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "Shell", out wBuffer))
                    wLog.WriteLine(@"HKCU\...\Winlogon => Shell: {0}", wBuffer);
                if (GetStringValue(HKLM_Winlogon, "LogonPrompt", out wBuffer))
                    wLog.WriteLine("LogonPrompt: {0}", wBuffer);
                if (GetStringValue(HKLM_Winlogon, "System", out wBuffer))
                    wLog.WriteLine("System: {0}", wBuffer);
                if (GetStringValue(HKLM_Winlogon, "AppSetup", out wBuffer))
                    wLog.WriteLine("AppSetup: {0}", wBuffer);
                if (GetStringValue(HKLM_Winlogon, "Taskman", out wBuffer))
                    wLog.WriteLine("VmApplet: {0}", wBuffer);
                if (GetStringValue(HKLM_Winlogon, "GinaDLL", out wBuffer))
                    wLog.WriteLine("GinaDLL: {0}", wBuffer);
                if (GetStringValue(HKLM_Winlogon, "UIHost", out wBuffer))
                    wLog.WriteLine("UIHost: {0}", wBuffer);
                if (GetSubkeys(HKLM_Winlogon_Notify, out wBuffers))
                    for (int i = 0; i < wBuffers.Length; i++)
                    {
                        wLog.WriteLine("Winlogon Notify: {0}", wBuffers[i]);
                        if (GetStringValue(HKLM_Winlogon_Notify + wBuffers[i], "DllName", out wBuffer))
                        {
                            string wPath;

                            if (wBuffer.Split(cSlash).Length > 1)
                                wPath = wBuffer;
                            else
                                wPath = GetSystem32Path() + "\\" + wBuffer;
                            
                            if (!File.Exists(wPath))
                                wLog.WriteLine("Файл не найден: {0}", wPath);
                            else
                            {
                                wLog.WriteLine("Путь к файлу: {0}", wPath);
                                GetFileInfo(wPath, false);
                            }

                            wPath = null;
                        }
                        else
                            wLog.WriteLine("[!] Не удалось получить информацию из реестра");
                    }
            }
            catch { }
            #endregion
            #region Explorer
            wLog.WriteLine("--------------------------------------------{0}--------------------------------------------", @"Настройки Проводника");
            try
            {
                string[] wBuffers = null;
                string wBuffer = null;
                if (GetSubkeys(HKLM_EX_Filters, out wBuffers))
                    for (int i = 0; i < wBuffers.Length; i++)
                    {
                        wLog.WriteLine("Фильтр проводника: {0}", wBuffers[i]);
                        if (GetStringValue(HKLM_EX_Filters + "\\" + wBuffers[i], "CLSID", out wBuffer))
                        {
                            wLog.WriteLine("    CLSID: {0}", wBuffer);
                            GetCLSIDInfo(wBuffer);
                        }
                    }
                if (GetSubkeys(HKLM_EX_Handlers, out wBuffers))
                    for (int i = 0; i < wBuffers.Length; i++)
                    {
                        wLog.WriteLine("Handler проводника: {0}", wBuffers[i]);
                        if (GetStringValue(HKLM_EX_Handlers + "\\" + wBuffers[i], "CLSID", out wBuffer))
                        {
                            wLog.WriteLine("    CLSID: {0}", wBuffer);
                            GetCLSIDInfo(wBuffer);
                        }
                    }
                if (GetValueNames(HKLM_EX_SEH, out wBuffers))
                    for (int i = 0; i < wBuffers.Length; i++)
                    {
                        wLog.WriteLine("Хук ShellExecute: {0}", wBuffers[i]);
                        if (GetStringValue(HKLM_EX_SEH, wBuffers[i], out wBuffer))
                        {
                            wLog.WriteLine("    Файл: {0}", wBuffer);
                            GetFileInfo(wBuffer, false);
                        }
                    }
                wBuffer = null;
                wBuffers = null;
            }
            catch { }
            #endregion
            #region AppInit_DLLs
            wLog.WriteLine("--------------------------------------------{0}--------------------------------------------", @"AppInit_DLLs");
            try
            {
                string wDLL = "";
                string[] wDLLs = null;
                if (GetStringValue(HKLM_Windows, "AppInit_DLLs", out wDLL))
                {
                    wDLLs = wDLL.Split(' ');
                    for (int i = 0; i < wDLLs.Length; i++)
                        if (NotNull(wDLLs[i]))
                        {
                            string wPath = wDLLs[i].Replace(",", "").Replace("\"", "");
                            wLog.WriteLine("AppInit_DLL: {0}", wPath);
                            wPath = ShortToLongFilename(wPath);
                            GetFileInfo(wPath);
                            wPath = null;
                        }
                    wDLL = null;
                }
                if (Is64Bit())
                {
                    string wDLL64 = "";
                    if (GetStringValue(HKLM_Windows64, "AppInit_DLLs", out wDLL64))
                    {
                        string[] wDLLs64 = wDLL64.Split(' ');
                        for (int i = 0; i < wDLLs64.Length; i++)
                            if (NotNull(wDLLs64[i]))
                            {
                                string wPath = ShortToLongFilename(wDLLs64[i].Replace(",", "").Replace("\"", ""));
                                wLog.WriteLine("AppInit_DLL: {0}", wPath);
                                wPath = ShortToLongFilename(wPath);
                                GetFileInfo(wPath);
                                wPath = null;
                            }
                        wDLLs64 = null;
                    }
                }
                wDLL = null;
                wDLLs = null;
            }
            catch { }
            #endregion
            #region SSODLs
            try
            {
                string[] wSSODLs = null;
                string wBuffer = null;
                bool wBuf = false;

                if (GetValueNames(HKLM_SSODL, out wSSODLs))
                    if (wSSODLs.Length > 0)
                    {
                        wLog.WriteLine("--------------------------------------------{0}--------------------------------------------", "SSODL (Shell Service Object Delay Load)");
                        if (GetValueNames(HKLM_SSODL, out wSSODLs))
                            for (int i = 0; i < wSSODLs.Length; i++)
                                if (NotNull(wSSODLs[i]))
                                {
                                    wLog.WriteLine("SSODL: {0}", wSSODLs[i]);
                                    if (GetStringValue(HKLM_SSODL, wSSODLs[i], out wBuffer))
                                    {
                                        wLog.WriteLine(" CLSID: {0}", wBuffer);
                                        GetCLSIDInfo(wBuffer);
                                    }
                                }
                        wBuf = true;
                    }
                if (GetValueNames(HKCU_SSODL, out wSSODLs))
                    if (wSSODLs.Length > 0)
                    {
                        if (!wBuf)
                            wLog.WriteLine("--------------------------------------------{0}--------------------------------------------", "SSODL (Shell Service Object Delay Load)");
                        for (int i = 0; i < wSSODLs.Length; i++)
                            if (NotNull(wSSODLs[i]))
                            {
                                wLog.WriteLine("SSODL: {0}", wSSODLs[i]);
                                if (GetStringValue(HKCU_SSODL, wSSODLs[i], out wBuffer))
                                {
                                    wLog.WriteLine(" CLSID: {0}", wBuffer);
                                    GetCLSIDInfo(wBuffer);
                                }
                            }
                    }

                wBuffer = null;
                wSSODLs = null;
            }
            catch { }
            #endregion
            #region HKCU(HKLM)\...\Policies
            wLog.WriteLine("--------------------------------------------{0}--------------------------------------------", @"Ограничения (HKCU[HKLM]\...\Policies)");
            try
            {
                string[] wValues = null;
                #region Policies\Explorer
                if (GetValueNames(HKCU_Policies_Explorer, out wValues))
                    for (int i = 0; i < wValues.Length; i++)
                        if (NotNull(wValues[i]))
                            CheckPolicies(HKCU_Policies_Explorer, wValues[i], wLog);
                #endregion Policies\Explorer
                #region Policies\Explorer\DisallowRun
                if (GetValueNames(HKCU_Explorer_DisallowRun, out wValues))
                    for (int i = 0; i < wValues.Length; i++)
                        if (NotNull(wValues[i]))
                        {
                            string wBuffer = null;
                            wLog.WriteLine(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun => " +
                                (GetStringValue(HKCU_Explorer_DisallowRun, wValues[i], out wBuffer) ? wValues[i] + " => " +
                                wBuffer + " (Запрет запуска файла)" : wValues[i] + " (Запрет запуска файла)"));
                            wBuffer = null;
                        }
                #endregion Policies\Explorer\DisallowRun
                #region Policies\Explorer\RestrictRun
                if (GetValueNames(HKCU_Explorer_RestrictRun, out wValues))
                    for (int i = 0; i < wValues.Length; i++)
                        if (NotNull(wValues[i]))
                        {
                            string wBuffer = null;
                            wLog.WriteLine(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\RestrictRun => " +
                                (GetStringValue(HKCU_Explorer_RestrictRun, wValues[i], out wBuffer) ? wValues[i] +
                                " => " + wBuffer + " (Запрет запуска файлов, кроме указанного(ых) в данной ветке реестра)"
                                : wValues[i] + " (Запрет запуска файлов, кроме указанного(ых) в данной ветке реестра)"));
                            wBuffer = null;
                        }
                #endregion Policies\Explorer\DisallowRun
                #region Policies\System
                if (GetValueNames(HKCU_Policies_System, out wValues))
                    for (int i = 0; i < wValues.Length; i++)
                        if (NotNull(wValues[i]))
                            CheckPolicies(HKCU_Policies_System, wValues[i], wLog);
                #endregion Policies\System
                #region Policies\Microsoft\IE\Restrictions
                if (GetValueNames(HKCU_Policies_IE_Restrictions, out wValues))
                    for (int i = 0; i < wValues.Length; i++)
                        if (NotNull(wValues[i]))
                            CheckPolicies(HKCU_Policies_IE_Restrictions, wValues[i], wLog);
                #endregion Policies\Microsoft\IE\Restrictions

                #region Policies\Explorer
                if (GetValueNames(HKLM_Policies_Explorer, out wValues))
                    for (int i = 0; i < wValues.Length; i++)
                        if (NotNull(wValues[i]))
                            CheckPolicies(HKLM_Policies_Explorer, wValues[i], wLog);
                #endregion Policies\Explorer
                #region Policies\Explorer\DisallowRun
                if (GetValueNames(HKLM_Explorer_DisallowRun, out wValues))
                    for (int i = 0; i < wValues.Length; i++)
                        if (NotNull(wValues[i]))
                        {
                            string wBuffer = null;
                            wLog.WriteLine(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun => " +
                                (GetStringValue(HKLM_Explorer_DisallowRun, wValues[i], out wBuffer) ? wValues[i] + " => " +
                                wBuffer + " (Запрет запуска файла)" : wValues[i] + " (Запрет запуска файла)"));
                            wBuffer = null;
                        }
                #endregion Policies\Explorer\DisallowRun
                #region Policies\Explorer\RestrictRun
                if (GetValueNames(HKLM_Explorer_RestrictRun, out wValues))
                    for (int i = 0; i < wValues.Length; i++)
                        if (NotNull(wValues[i]))
                        {
                            string wBuffer = null;
                            wLog.WriteLine(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\RestrictRun => " +
                                (GetStringValue(HKLM_Explorer_RestrictRun, wValues[i], out wBuffer) ? wValues[i] +
                                " => " + wBuffer + " (Запрет запуска файлов, кроме указанного(ых) в данной ветке реестра)"
                                : wValues[i] + " (Запрет запуска файлов, кроме указанного(ых) в данной ветке реестра)"));
                            wBuffer = null;
                        }
                #endregion Policies\Explorer\DisallowRun
                #region Policies\System
                if (GetValueNames(HKLM_Policies_System, out wValues))
                    for (int i = 0; i < wValues.Length; i++)
                        if (NotNull(wValues[i]))
                            CheckPolicies(HKLM_Policies_System, wValues[i], wLog);
                #endregion Policies\System
                wValues = null;
            }
            catch { }
            #endregion
            #region TCP/IP
            wLog.WriteLine("--------------------------------------------{0}--------------------------------------------", @"HKLM\...\Tcpip\Parameters");
            try
            {
                string[] wValueNames = null;
                WriteAllValues(HKLM_CCS_TCPIP_Params);
                if (GetValueNames(HKLM_CCS_TCPIP_PR, out wValueNames))
                    for (int i = 0; i < wValueNames.Length; i++)
                        wLog.WriteLine("Статический маршрут TCP/IP: {0}", wValueNames[i]);
                wValueNames = null;
            }
            catch { }
            #endregion
            #region LSA
            try
            {
                bool wBuf = false;
                string wBuffer;
                if (GetStringValue(HKLM_CCS_LSA, "Authentication Packages", out wBuffer))
                {
                    wLog.WriteLine("--------------------------------------------{0}--------------------------------------------", "Настройки LSA");
                    wLog.WriteLine("Authentication Packages: {0}", wBuffer);
                    wBuf = true;
                }
                if (GetStringValue(HKLM_CCS_LSA, "Notification Packages", out wBuffer))
                {
                    if (!wBuf)
                    {
                        wLog.WriteLine("--------------------------------------------{0}--------------------------------------------", "Настройки LSA");
                        wBuf = true;
                    }
                    wLog.WriteLine("Notification Packages: {0}", wBuffer);
                }
                if (GetStringValue(HKLM_CCS_LSA, "Security Packages", out wBuffer))
                {
                    if (!wBuf)
                    {
                        wLog.WriteLine("--------------------------------------------{0}--------------------------------------------", "Настройки LSA");
                        wBuf = true;
                    }
                    wLog.WriteLine("Security Packages: {0}", wBuffer);
                }
                if (GetStringValue(HKLM_CCS_SP, "SecurityProviders", out wBuffer))
                {
                    if (!wBuf)
                    {
                        wLog.WriteLine("--------------------------------------------{0}--------------------------------------------", "Настройки LSA");
                        wBuf = true;
                    }
                    wLog.WriteLine("SecurityProviders: {0}", wBuffer);
                }
                wBuffer = null;
            }
            catch { }
            #endregion
            #region IE
            wLog.WriteLine("--------------------------------------------{0}--------------------------------------------", @"Internet Explorer (HKCU\...\Internet Explorer | HKLM\...\Internet Explorer)");
            try
            {
                string[] wValues = null;
                string wStrBuff = null;
                int wIntBuff = -1;
                if (GetStringValue(HKLM_IE, "Version", out wStrBuff))
                    wLog.WriteLine("Версия IE: {0}", wStrBuff);
                if (GetStringValue(HKLM_IE_Main, "Search Page", out wStrBuff))
                    wLog.WriteLine("Страница поиска: {0}", wStrBuff);
                if (GetStringValue(HKLM_IE_Main, "Start Page", out wStrBuff))
                    wLog.WriteLine("Стартовая страница: {0}", wStrBuff);
                #region URLHooks
                if (GetValueNames(HKCU_IE_URLHooks, out wValues))
                    for (int i = 0; i < wValues.Length; i++)
                    {
                        wLog.WriteLine("URLSearchHook: {0}", wValues[i]);
                        GetCLSIDInfo(wValues[i]);
                    }
                #endregion URLHooks
                #region IERestrictions
                if (GetValueNames(@"HKCU\Software\Policies\Microsoft\Internet Explorer\Restrictions", out wValues))
                {
                    wLog.WriteLine(@"Обнаружены ограничения: HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Restrictions");
                    for (int i = 0; i < wValues.Length; i++)
                        CheckPolicies(@"HKCU\Software\Policies\Microsoft\Internet Explorer\Restrictions", wValues[i], wLog);
                }
                if (GetValueNames(@"HKCU\Software\Policies\Microsoft\Internet Explorer\Control Panel", out wValues))
                {
                    wLog.WriteLine(@"Обнаружены ограничения: HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel");
                    for (int i = 0; i < wValues.Length; i++)
                        CheckPolicies(@"HKCU\Software\Policies\Microsoft\Internet Explorer\Control Panel", wValues[i], wLog);
                }
                if (GetValueNames(@"HKCU\Software\Policies\Microsoft\Internet Explorer\Toolbars\Restrictions", out wValues))
                {
                    wLog.WriteLine(@"Обнаружены ограничения: HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Toolbars\Restrictions");
                    for (int i = 0; i < wValues.Length; i++)
                        CheckPolicies(@"HKCU\Software\Policies\Microsoft\Internet Explorer\Toolbars\Restrictions", wValues[i], wLog);
                }
                if (GetValueNames(@"HKLM\Software\Policies\Microsoft\Internet Explorer\Restrictions", out wValues))
                {
                    wLog.WriteLine(@"Обнаружены ограничения: HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Internet Explorer\Restrictions");
                    for (int i = 0; i < wValues.Length; i++)
                        CheckPolicies(@"HKLM\Software\Policies\Microsoft\Internet Explorer\Restrictions", wValues[i], wLog);
                }
                if (GetValueNames(@"HKLM\Software\Policies\Microsoft\Internet Explorer\Control Panel", out wValues))
                {
                    wLog.WriteLine(@"Обнаружены ограничения: HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Internet Explorer\Control Panel");
                    for (int i = 0; i < wValues.Length; i++)
                        CheckPolicies(@"HKLM\Software\Policies\Microsoft\Internet Explorer\Control Panel", wValues[i], wLog);
                }
                if (GetValueNames(@"HKLM\Software\Policies\Microsoft\Internet Explorer\Toolbars\Restrictions", out wValues))
                {
                    wLog.WriteLine(@"Обнаружены ограничения: HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Internet Explorer\Toolbars\Restrictions");
                    for (int i = 0; i < wValues.Length; i++)
                        CheckPolicies(@"HKLM\Software\Policies\Microsoft\Internet Explorer\Toolbars\Restrictions", wValues[i], wLog);
                }
                #endregion IERestrictions
                #region Toolbars
                if (GetValueNames(HKLM_IE_Toolbars, out wValues))
                    for (int i = 0; i < wValues.Length; i++)
                    {
                        wLog.WriteLine("Панель инструментов: {0}", 
                            (GetStringValue(HKLM_IE_Toolbars, wValues[i], out wStrBuff) ? wStrBuff : "???"));
                        wLog.WriteLine("CLSID: {0}", wValues[i]);
                        GetCLSIDInfo(wValues[i]);
                    }
                #endregion Toolbars
                #region Extensions
                if (GetSubkeys(HKCU_IE_MenuExts, out wValues))
                    for (int i = 0; i < wValues.Length; i++)
                    {
                        wLog.WriteLine("Расширение меню для IE: {0}", wValues[i]);
                        wLog.WriteLine("Путь: {0}", (GetStringValue(HKCU_IE_MenuExts + "\\" + wValues[i], "", out wStrBuff) ? wStrBuff : "???"));
                    }
                if (GetSubkeys(HKLM_IE_Exts, out wValues))
                    for (int i = 0; i < wValues.Length; i++)
                    {
                        wLog.WriteLine("Расширение меню для IE: {0}",
                            (GetStringValue(HKLM_IE_Exts + "\\" + wValues[i], "MenuText", out wStrBuff) ? wStrBuff : "???"));
                        wLog.WriteLine("CLSID: {0}", wValues[i]);
                        GetCLSIDInfo(wValues[i]);
                    }
                #endregion Extensions
                #region Advanced Settings
                if (GetSubkeys(HKLM_IE_AS, out wValues))
                    for (int i = 0; i < wValues.Length; i++)
                        wLog.WriteLine("Дополнительное меню настроек: [" + wValues[i] + "] " +
                            (GetStringValue(HKLM_IE_AS, "Text", out wStrBuff) ? wStrBuff : ""));
                #endregion Advanced Settings
                #region Settings
                if (GetIntValue(HKCU_IE_Settings, "ProxyEnable", out wIntBuff))
                    if (wIntBuff != 1)
                        wLog.WriteLine("Прокси отключен.");
                    else
                    {
                        wLog.WriteLine("Прокси включен.");
                        if (GetStringValue(HKCU_IE_Settings, "ProxyServer", out wStrBuff))
                            wLog.WriteLine("Прокси: {0}", wStrBuff);
                        else
                            wLog.WriteLine("Не удалось получить адрес прокси-сервера.");
                    }
                if (GetIntValue(HKLM_IE_Main, "DoNotTrack", out wIntBuff))
                    wLog.WriteLine(wIntBuff != 1 ? "Режим Do Not Track отключен." : "Режим Do Not Track включен.");
                #endregion Settings
                wStrBuff = null;
                wIntBuff = 0;
                wValues = null;
            }
            catch { }
            #endregion
            #region Browsers
            wLog.WriteLine("--------------------------------------------{0}--------------------------------------------", "Браузеры сторонних разработчиков");
            try
            {
                string wBuffer = null;
                string[] wValues = null;
                if (GetStringValue(HKLM_CH_Ver, "version", out wBuffer))
                    wLog.WriteLine("Версия Google Chrome: {0}", wBuffer);
                else if (GetStringValue(HKLM_CH_Ver2, "pv", out wBuffer))
                    wLog.WriteLine("Версия Google Chrome: {0}", wBuffer);
                if (GetSubkeys(HKLM_CH_Exts, out wValues))
                    for (int i = 0; i < wValues.Length; i++)
                    {
                        wLog.WriteLine("Расширение для Chrome: {0}", wValues[i]);
                        if (GetStringValue(HKLM_CH_Exts + "\\" + wValues[i], "path", out wBuffer))
                            wLog.WriteLine("{0} => {1}", wValues[i], wBuffer);
                    }
                wLog.WriteLine();
                wBuffer = null;
                wValues = null;
            }
            catch { }
            try
            {
                string a = null;
                string[] values = null;
                if (GetStringValue(HKLM_FF, "CurrentVersion", out a))
                    wLog.WriteLine("Версия Mozilla Firefox: {0}", a);
                else if (GetStringValue(HKLM_FF2, "CurrentVersion", out a))
                    wLog.WriteLine("Версия Mozilla Firefox: {0}", a);
                if (GetSubkeys(HKLM_FF_Plugins, out values))
                    foreach (string extension in values)
                    {
                        wLog.WriteLine("Плагин Mozilla Firefox: {0}", extension);
                        if (GetStringValue(HKLM_FF_Plugins + "\\" + extension, "Description", out a))
                            wLog.WriteLine("    Описание: {0}", a);
                        if (GetStringValue(HKLM_FF_Plugins + "\\" + extension, "XPTPath", out a))
                            wLog.WriteLine("    Путь к файлу XPT: {0}", a);
                    }
                if (GetSubkeys(HKCU_FF_Plugins, out values))
                    foreach (string extension in values)
                    {
                        wLog.WriteLine("Плагин Mozilla Firefox: {0}", extension);
                        if (GetStringValue(HKCU_FF_Plugins + "\\" + extension, "Description", out a))
                            wLog.WriteLine("    Описание: {0}", a);
                        if (GetStringValue(HKCU_FF_Plugins + "\\" + extension, "XPTPath", out a))
                            wLog.WriteLine("    Путь к файлу XPT: {0}", a);
                    }
                a = null;
                values = null;
            }
            catch { }
            try
            {
                string a = null;
                string[] values = null;
                if (GetStringValue(HKCU_YA_Ver, "version", out a))
                    wLog.WriteLine("Версия Яндекс.Браузера: {0}", a);
                if (GetSubkeys(HKLM_YA_Exts, out values))
                    foreach (string extension in values)
                    {
                        wLog.WriteLine("Расширение для Яндекс.Браузера: {0}", extension);
                        if (GetStringValue(HKLM_YA_Exts + "\\" + extension, "path", out a))
                            wLog.WriteLine("{0} => {1}", extension, a);
                    }
                wLog.WriteLine();
                a = null;
                values = null;
            }
            catch { }
            #endregion
            #region Modules
            wLog.WriteLine("--------------------------------------------{0}--------------------------------------------", "Внедренные модули");
            try
            {
                ProcessModuleCollection wModules = Process.GetCurrentProcess().Modules;
                for (int i = 0; i < wModules.Count; i++)
                {
                    ProcessModule wModule = wModules[i];
                    string wPath = wModule.FileName;
                    wLog.WriteLine("Внедренный модуль: {0}", NotNull(wPath) ? wPath : wModule.ModuleName);
                    if (NotNull(wModule.EntryPointAddress.ToString("x2")))
                        wLog.WriteLine("    Адрес в памяти: {0}", "[0x" + wModule.EntryPointAddress.ToString("x2") + "]");
                    if (NotNull(wPath))
                        GetFileInfo(wPath);
                    wModule.Dispose();
                }
                wModules = null;
            }
            catch { }
            #endregion
            #region Hosts
            wLog.WriteLine("--------------------------------------------{0}--------------------------------------------", "Файл HOSTS");
            try
            {
                IntPtr hFile = ZCreateFile(GetHostsFolder().Replace("%SystemRoot%", GetWindowsPath()) + @"\hosts", GENERIC_READ,
                    0, 0, OPEN_EXISTING, 0, 0);
                if (hFile == IntPtr.Zero)
                    wLog.WriteLine("[!] Не удалось открыть файл HOSTS");
                ///////////////////////////////////////////////////////////////////
                byte[] wBuffer = new byte[128];
                UTF8Encoding wEncoding = new UTF8Encoding();
                ///////////////////////////////////////////////////////////////////
                int wBytesRead;
                do
                {
                    wBytesRead = FileRead(hFile, wBuffer, 0, wBuffer.Length);
                    wLog.Write(wEncoding.GetString(wBuffer, 0, wBytesRead));
                } while (wBytesRead > 0);
                wLog.WriteLine(wBytesRead < 2 ? "Файл пуст" : "");
            }
            catch { }
            #endregion
            #region AppDataFolder
            wLog.WriteLine("--------------------------------------------{0}--------------------------------------------", "Файлы и папки: %AppData%(Roaming)");
            wLog.WriteLine("[F] - файл | [D] - директория");
            try
            {
                string wAppData = Environment.GetEnvironmentVariable("AppData");
                string[] wObjects = Directory.GetDirectories(wAppData);
                ///////////////////////////////////////////////////////////////
                for (int i = 0; i < wObjects.Length; i++)
                {
                    var wObject = wObjects[i];
                    if (NotNull(wObject))
                        wLog.WriteLine("[D] {0}", wObject);
                    wObject = null;
                }
                wObjects = Directory.GetFiles(wAppData);
                for (int i = 0; i < wObjects.Length; i++)
                {
                    var wObject = wObjects[i];
                    if (NotNull(wObject))
                        wLog.WriteLine("[F] {0}", wObject);
                    wObject = null;
                }
                ///////////////////////////////////////////////////////////////
                wObjects = null;
                wAppData = null;
            }
            catch { }
            #endregion

            wLog.WriteLine("--------------------------------------------{0}--------------------------------------------", "Протокол");
            wLog.WriteLine("Время: {0}", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            wLog.WriteLine("Папка запуска: {0}", wStartupPath);

            wLog.WriteLine();

            switch (ZGetSystemMetrics(SM_CLEANBOOT))
            {
                case 0:
                    wLog.WriteLine("Режим загрузки Windows: Обычный");
                    break;
                case 1:
                    wLog.WriteLine("Режим загрузки Windows: Безопасный");
                    break;
                case 2:
                    wLog.WriteLine("Режим загрузки Windows: Безопасный с загрузкой сетевых драйверов");
                    break;
                default:
                    wLog.WriteLine("Не удалось получить информацию о режиме загрузки.");
                    break;
            }

            ManagementObjectSearcher wSysInfo = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_OperatingSystem");
            foreach (ManagementObject wQueryObj in wSysInfo.Get())
                wLog.WriteLine("Версия системы: {0} {1}", wQueryObj["Caption"], wQueryObj["Version"]);
            wSysInfo.Dispose();
            ////////////////////////////////////////////////////////////
            wLog.WriteLine(Is64Bit() ? "Разрядность: x64" : "Разрядность: x32");
            wLog.WriteLine("Папка WINDOWS: {0}", GetWindowsPath());
            wLog.WriteLine("Папка System32: {0}", GetSystem32Path());
            wLog.WriteLine("Системный диск: {0}", GetWindowsPath().Split(cSlash)[0] + "\\");
            ////////////////////////////////////////////////////////////
            wLog.WriteLine(IsServiceStarted("MpsSvc") ? "Брандмауэр Windows работает." :
                "Брандмауэр Windows выключен.");
            wLog.WriteLine(IsServiceStarted("wuauserv") ? "Служба центра обновления Windows работает." :
                "[!] Служба центра обновления Windows выключена.");
            wLog.WriteLine(IsServiceStarted("wscsvc") ? "Служба центра обеспечения безопасности Windows работает." :
                "[!] Служба центра обновления Windows выключена.");
            wLog.WriteLine(IsServiceStarted("RemoteRegistry") ? "[!] Удаленный реестр работает." :
                "Удаленный реестр выключен.");
            wLog.WriteLine();

            GetExtensionInfo("bat");
            GetExtensionInfo("cmd");
            GetExtensionInfo("com");
            GetExtensionInfo("exe");
            GetExtensionInfo("hta");
            GetExtensionInfo("jse");
            GetExtensionInfo("js");
            GetExtensionInfo("lnk");
            GetExtensionInfo("pif");
            GetExtensionInfo("scr");
            GetExtensionInfo("vbe");
            GetExtensionInfo("vbs");
            GetExtensionInfo("wsh");
            wLog.WriteLine();

            TimeSpan wResult = DateTime.UtcNow - wStartTime;
            wLog.Write("Создание лога завершено({0} сек)", Math.Round(wResult.TotalSeconds, 2));
            
            wLog.Close();
            wLog.Dispose();

            ShellExecute(IntPtr.Zero, "open", "notepad.exe", wStartupPath + "\\WinCheck.log", null, 3); // 3 => SW_MAXIMIZE
            wStartupPath = null;
        }
    }
    static class CheckSvc
    {
        static string wWindows = GetWindowsPath().ToLower();
        static string wSystem32 = GetSystem32Path().ToLower();
        ///////////////////////////////////////////////////////////////////////
        #region Pathes
        static string[] wPathes = {
            wSystem32 + @"\svchost.exe -k LocalService",
            wSystem32 + @"\alg.exe",
            wSystem32 + @"\svchost.exe -k LocalServiceNetworkRestricted",
            wSystem32 + @"\svchost.exe -k netsvcs",
            wSystem32 + @"\svchost.exe -k AppReadiness",
            wSystem32 + @"\svchost.exe -k wsappx",
            wSystem32 + @"\svchost.exe -k AxInstSVGroup",
            wSystem32 + @"\svchost.exe -k LocalServiceNoNetwork",
            wSystem32 + @"\svchost.exe -k DcomLaunch",
            wSystem32 + @"\svchost.exe -k LocalServiceAndNoImpersonation",
            wSystem32 + @"\svchost.exe -k NetworkService",
            wSystem32 + @"\svchost.exe -k defragsvc",
            wSystem32 + @"\svchost.exe -k appmodel",
            wSystem32 + @"\fxssvc.exe",
            wSystem32 + @"\lsass.exe",
            wSystem32 + @"\msiexec.exe /V",
            wSystem32 + @"\svchost.exe -k LocalServicePeerNet",
            wSystem32 + @"\svchost.exe -k PeerDist",
            wSystem32 + @"\svchost.exe -k print",
            wSystem32 + @"\svchost.exe -k RPCSS",
            wSystem32 + @"\locator.exe",
            wSystem32 + @"\SensorDataService.exe",
            wSystem32 + @"\svchost.exe -k smphost",
            wSystem32 + @"\snmptrap.exe",
            wSystem32 + @"\spoolsv.exe",
            wSystem32 + @"\sppsvc.exe",
            wSystem32 + @"\svchost.exe -k imgsvc",
            wSystem32 + @"\svchost.exe -k swprv",
            wSystem32 + @"\svchost.exe -k LocalSystemNetworkRestricted",
            wSystem32 + @"\UI0Detect.exe",
            wSystem32 + @"\vds.exe",
            wSystem32 + @"\svchost.exe -k ICService",
            wSystem32 + @"\vssvc.exe",
            wSystem32 + @"\wbengine.exe",
            wSystem32 + @"\svchost.exe -k WbioSvcGroup",
            wSystem32 + @"\svchost.exe -k WepHostSvcGroup",
            wSystem32 + @"\svchost.exe -k WerSvcGroup",
            wSystem32 + @"\wbem\WmiApSrv.exe",
            wSystem32 + @"\svchost.exe -k wswpnservice",
            wSystem32 + @"\SearchIndexer.exe /Embedding",
            wSystem32 + @"\svchost.exe -k NetworkServiceAndNoImpersonation",
            wSystem32 + @"\svchost.exe -k NetworkServiceNetworkRestricted",
            wSystem32 + @"\svchost.exe -k SDRSVC",
            wSystem32 + @"\svchost.exe -k wcssvc",
            wSystem32 + @"\clipsrv.exe",
            wWindows + @"\servicing\TrustedInstaller.exe",
        };
        #endregion Pathes
        ///////////////////////////////////////////////////////////////////////
        public static bool PathInWhiteList(string pPath)
        {
            try
            {
                for (int i = 0; i < wPathes.Length; i++)
                    if (pPath.Replace("\"", "").ToLower() == wPathes[i].ToLower())
                        return true;
            }
            catch { }
            return false;
        }
    }
    static class Policies
    {
        [Flags()]
        enum AutorunFlags
        {
            UnknownTypes = 0x1,
            RemovableDrives = 0x4,
            NonRemovableDrives = 0x8,
            NetworkDrives = 0x10,
            CDDrives = 0x20,
            RAMDisks = 0x40,
            Unknown = 0x80,
            All = 0xFF
        }
        public static void CheckPolicies(string pKey, string pValueName, StreamWriter pLog)
        {
            int wBuffer = 0;
            #region Policies\Microsoft\Internet Explorer\Control Panel
            if (pKey.ToLower() == @"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel".ToLower() || pKey == @"HKCU\Software\Policies\Microsoft\Internet Explorer\Control Panel".ToLower())
            {
                switch (pValueName)
                {
                    case "AutoConfig":
                        if (GetIntValue(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel",
                            "AutoConfig", out wBuffer) && wBuffer == 1)
                            pLog.WriteLine(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel => AutoConfig => 1 (Свойства обозревателя\Подключения\Настройка локальной сети (LAN) (Internet Options\Connections\Local Area Network (LAN) Setting) - Блокировка доступа к изменению Автоматических настроек)");
                        break;
                    case "CalendarContact":
                        if (GetIntValue(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel",
                            "CalendarContact", out wBuffer) && wBuffer == 1)
                            pLog.WriteLine(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel => CalendarContact => 1 (Блокировка доступа к изменению Календаря и Адресной книги)");
                        break;
                    case "Certificates":
                        if (GetIntValue(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel",
                            "Certificates", out wBuffer) && wBuffer == 1)
                            pLog.WriteLine(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel => Certificates => 1 (Свойства обозревателя\Содержание (Internet Options\Content) - Блокировка доступа к кнопкам 'Сертификаты' и 'Издатели')");
                        break;
                    case "CertifPers":
                        if (GetIntValue(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel",
                            "CertifPers", out wBuffer) && wBuffer == 1)
                            pLog.WriteLine(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel => CertifPers => 1 (Свойства обозревателя\Содержание (Internet Options\Content) - Блокировка доступа к кнопке 'Сертификаты')");
                        break;
                    case "CertifPub":
                        if (GetIntValue(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel",
                            "CertifPub", out wBuffer) && wBuffer == 1)
                            pLog.WriteLine(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel => CertifPub => 1 (Свойства обозревателя\Содержание (Internet Options\Content) - Блокировка доступа к кнопке 'Издатели')");
                        break;
                    case "Check_If_Default":
                        if (GetIntValue(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel",
                            "Check_If_Default", out wBuffer) && wBuffer == 1)
                            pLog.WriteLine(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel => Check_If_Default => 1 (Свойства обозревателя\Программы (Internet Options\Programs) - блокирует доступ к изменению параметра 'Проверять, является ли Internet Explorer используемым по умолчанию обозревателем')");
                        break;
                    case "Colors":
                        if (GetIntValue(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel",
                            "Colors", out wBuffer) && wBuffer == 1)
                            pLog.WriteLine(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel => Colors => 1 (Свойства обозревателя\Общие\Цвета... (Internet Options\General\Colors...) - Блокировка доступа к изменению параметра 'Цвета')");
                        break;
                    case "Connection Settings":
                        if (GetIntValue(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel",
                            "Connection Settings", out wBuffer) && wBuffer == 1)
                            pLog.WriteLine(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel => Connection Settings => 1 (Свойства обозревателя\Подключения (Internet Options\Connections) - Блокировка доступа к изменению параметров 'Подключения', кроме кнопки Установки нового подключения)");
                        break;
                    case "Accessibility":
                        if (GetIntValue(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel",
                            "Accessibility", out wBuffer) && wBuffer == 1)
                            pLog.WriteLine(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel => Accessibility => 1 (Свойства обозревателя\Общие\Оформление... (Internet Options\General\Accessibility...) - Блокировка доступа к изменению параметров Оформления (Accessibility))");
                        break;
                    case "Fonts":
                        if (GetIntValue(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel",
                            "Fonts", out wBuffer) && wBuffer == 1)
                            pLog.WriteLine(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel => Fonts => 1 (Свойства обозревателя\Общие\Оформление... (Свойства обозревателя\Общие\Шрифты... (Internet Options\General\Fonts...) - Блокировка доступа к изменению параметров Шрифтов (Fonts))");
                        break;
                    case "Languages":
                        if (GetIntValue(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel",
                            "Languages", out wBuffer) && wBuffer == 1)
                            pLog.WriteLine(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel => Languages => 1 (Свойства обозревателя\Общие\Оформление... (Свойства обозревателя\Общие\Языки... (Internet Options\General\Language...) - Блокировка доступа к изменению параметров Языки (Language))");
                        break;
                    case "Links":
                        if (GetIntValue(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel",
                            "Links", out wBuffer) && wBuffer == 1)
                            pLog.WriteLine(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel => Links => 1 (Свойства обозревателя\Общие\Цвета... (Internet Options\General\Colors...) - Блокировка доступа к изменению параметров Цвета на сслыки (Links))");
                        break;
                    case "Messaging":
                        if (GetIntValue(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel",
                            "Messaging", out wBuffer) && wBuffer == 1)
                            pLog.WriteLine(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel => Messaging => 1 (Свойства обозревателя\Общие\Цвета... (Internet Options\General\Colors...) - Блокировка доступа к изменению параметров Цвета на сслыки (Links))");
                        break;
                    case "Privacy Settings":
                        if (GetIntValue(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel",
                            "Privacy Settings", out wBuffer) && wBuffer == 1)
                            pLog.WriteLine(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel => Privacy Settings => 1 (Свойства обозревателя\Конфиденциальность (Internet Options\Privacy) - Блокировка доступа к изменению Параметров настройки (Settings))");
                        break;
                    case "Profiles":
                        if (GetIntValue(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel",
                            "Profiles", out wBuffer) && wBuffer == 1)
                            pLog.WriteLine(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel => Profiles => 1 (Свойства обозревателя\Содержание (Internet Options\Content) - Блокировка доступа к кнопке Профиль (My Profile))");
                        break;
                    case "Proxy":
                        if (GetIntValue(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel",
                            "Proxy", out wBuffer) && wBuffer == 1)
                            pLog.WriteLine(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel => Proxy => 1 (Свойства обозревателя\Подключения\Настройка локальной сети (LAN) (Internet Options\Connections\Local Area Network (LAN) Setting) - Блокировка доступа к изменению Прокси-сервер (Proxy server))");
                        break;
                    case "Ratings":
                        if (GetIntValue(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel",
                            "Ratings", out wBuffer) && wBuffer == 1)
                            pLog.WriteLine(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel => Ratings => 1 (Свойства обозревателя\Содержание (Internet Options\Content) - Блокировка доступа к кнопкам Ограничения доступа (Content Advisor))");
                        break;
                    case "SecAddSites":
                        if (GetIntValue(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel",
                            "SecAddSites", out wBuffer) && wBuffer == 1)
                            pLog.WriteLine(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel => SecAddSites => 1 (Свойства обозревателя\Безопасность (Internet Options\Security) - Запрет добавления сайтов для Безопасности)");
                        break;
                    case "SecChangeSettings":
                        if (GetIntValue(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel",
                            "SecChangeSettings", out wBuffer) && wBuffer == 1)
                            pLog.WriteLine(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Internet Explorer\Control Panel => SecChangeSettings => 1 (Свойства обозревателя\Безопасность (Internet Options\Security) - блокирует доступ к кнопкам Уровень безопасности для этой зоны (Security level for this zone))");
                        break;
                    default:
                        pLog.WriteLine("{0} => {1}", pKey, pValueName);
                        break;
                }
            }
            #endregion Policies\Microsoft\Internet Explorer\Control Panel
            else
            {
                if (GetIntValue(pKey, pValueName, out wBuffer))
                    switch (pValueName)
                    {
                        #region NoDriveTypeAutorun
                        case "NoDriveTypeAutoRun":
                            string wResult = null;
                            wResult = pKey + " => " + pValueName + " => " + wBuffer + " (Запрет автозапуска " + ((AutorunFlags)wBuffer).ToString()
                                .Replace("All", "со всех носителей").Replace("UnknownTypes", "для неизвестных устройств").Replace("RAMDisks", "для электронных дисков")
                                .Replace("CDDrives", "для компакт-дисков").Replace("NetworkDrives", "для сетевых дисков").Replace("NonRemovableDrives", "для несъемных дисков")
                                .Replace("RemovableDrives", "для съемных носителей").Replace("Unknown", "для дисков неизвестного типа") + ")";
                            pLog.WriteLine(wResult);
                            wResult = null;
                            break;
                        #endregion NoDriveTypeAutorun
                        case "DisableRegistryTools":
                            if (wBuffer == 1)
                                pLog.WriteLine("{0} => {1} => 1(Запрет запуска редактора реестра)", pKey, pValueName);
                            break;
                        case "DisableTaskMgr":
                            if (wBuffer == 1)
                                pLog.WriteLine("{0} => {1} => 1(Запрет запуска диспетчера задач)", pKey, pValueName);
                            break;
                        case "EnableLUA":
                            if (wBuffer == 1)
                                pLog.WriteLine("{0} => {1} => 1(Отключение UAC)", pKey, pValueName);
                            break;
                        case "DisableCMD":
                            if (wBuffer == 1)
                                pLog.WriteLine("{0} => {1} => 1(Отключение командной строки)", pKey, pValueName);
                            if (wBuffer == 2)
                                pLog.WriteLine("{0} => {1} => 2(Отключение командной строки, с разрешением запуска командных файлов)", pKey, pValueName);
                            break;
                        case "DisallowCpl":
                            if (wBuffer == 1)
                                pLog.WriteLine("{0} => {1} => 1(Отключение настроек проводника в панели управления)", pKey, pValueName);
                            break;
                        case "NoFolderOptions":
                            if (wBuffer == 1)
                                pLog.WriteLine("{0} => {1} => 1(Отключение настроек проводника в панели управления)", pKey, pValueName);
                            break;
                        case "NoBrowserContextMenu":
                            if (wBuffer == 1)
                                pLog.WriteLine("{0} => {1} => 1(Отключение контекстного меню в IE)", pKey, pValueName);
                            break;
                        case "TaskbarNoNotification":
                            if (wBuffer == 1)
                                pLog.WriteLine("{0} => {1} => 1(Отключение уведомлений)", pKey, pValueName);
                            break;
                        case "PromptOnSecureDesktop":
                            if (wBuffer == 1)
                                pLog.WriteLine("{0} => {1} => 1(Показывать диалог UAC на отдельном защищенном рабочем столе)", pKey, pValueName);
                            break;
                        #region Autorun
                        case "DisableLocalMachineRun":
                            if (wBuffer == 1)
                                pLog.WriteLine(@"{0} => {1} => 1(HKLM\...\Run: Отключение автозапуска)");
                            break;
                        case "DisableLocalUserRun":
                            if (wBuffer == 1)
                                pLog.WriteLine(@"{0} => {1} => 1(HKLM\...\Run: Отключение автозапуска)");
                            break;
                        case "DisableLocalMachineRunOnce":
                            if (wBuffer == 1)
                                pLog.WriteLine(@"{0} => {1} => 1(HKLM\...\RunOnce: Отключение автозапуска)");
                            break;
                        case "DisableCurrentUserRun":
                            if (wBuffer == 1)
                                pLog.WriteLine(@"{0} => {1} => 1(HKCU\...\Run: Отключение автозапуска)");
                            break;
                        case "DisableCurrentUserRunOnce":
                            if (wBuffer == 1)
                                pLog.WriteLine(@"{0} => {1} => 1(HKCU\...\RunOnce: Отключение автозапуска)");
                            break;
                        #endregion Autorun
                        default:
                            if (wBuffer == 1)
                                pLog.WriteLine("{0} => {1} => 1", pKey, pValueName);
                            break;
                    }
                else
                    pLog.WriteLine("{0} => {1}", pKey, pValueName);
            }
        }
        public static void CheckPolicies(Microsoft.Win32.RegistryKey pKey, string pValuename, StreamWriter pLog)
        {
            CheckPolicies(pKey.ToString(), pValuename, pLog);
        }
    }
}
