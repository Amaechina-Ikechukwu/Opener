using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

class Proccesses
{
    [DllImport("user32.dll")]
    private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll")]
    private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

    [DllImport("psapi.dll")]
    private static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, StringBuilder lpBaseName, int nSize);

    private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    public static void ListOfRunningProcesses()
    {
        Dictionary<string, string> openApps = new Dictionary<string, string>();
        EnumWindows(delegate (IntPtr hWnd, IntPtr lParam)
        {
            if (IsTaskbarWindow(hWnd))
            {
                StringBuilder sb = new StringBuilder(256);
                GetWindowText(hWnd, sb, sb.Capacity);
                string title = sb.ToString().Trim();
                if (!string.IsNullOrEmpty(title))
                {
                    uint processId;
                    GetWindowThreadProcessId(hWnd, out processId);
                    IntPtr hProcess = OpenProcess(0x0410, false, processId);
                    StringBuilder executablePath = new StringBuilder(256);
                    GetModuleFileNameEx(hProcess, IntPtr.Zero, executablePath, executablePath.Capacity);
                    string path = executablePath.ToString().Trim();
                    openApps[title] = path;
                }
            }
            return true;
        }, IntPtr.Zero);
        foreach (var apps in openApps)
        {
            Console.WriteLine($"Title: {apps.Key}, Executable Path: {apps.Value}");
            Console.WriteLine("");
            Console.WriteLine(" ");
        }
        SaveProcess.ReceiverInput(openApps);
    }

    private static bool IsTaskbarWindow(IntPtr hWnd)
    {
        const int GW_OWNER = 4;

        IntPtr shellWindow = GetShellWindow();
        if (hWnd == shellWindow) // Exclude the shell itself
            return false;

        IntPtr owner = GetWindow(hWnd, GW_OWNER);
        if (owner != IntPtr.Zero)
            return false; // Exclude owned windows

        int style = GetWindowLong(hWnd, GWL_STYLE);
        if ((style & WS_VISIBLE) == 0 || (style & WS_MINIMIZE) != 0)
            return false; // Exclude invisible or minimized windows

        if (!IsWindowVisible(hWnd))
            return false;

        uint processId;
        GetWindowThreadProcessId(hWnd, out processId);
        Process process = Process.GetProcessById((int)processId);

        if (process.ProcessName.ToLower() == "explorer")
            return false; // Exclude explorer windows

        return true;
    }

    // Constants for window styles
    private const int GWL_STYLE = -16;
    private const int WS_VISIBLE = 0x10000000;
    private const int WS_MINIMIZE = 0x20000000;

    [DllImport("user32.dll")]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern IntPtr GetShellWindow();

    [DllImport("user32.dll")]
    private static extern IntPtr GetWindow(IntPtr hWnd, int uCmd);
}
