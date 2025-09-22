using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VirtualizationButton.Models
{
    public class CommandManager
    {
        private static void RunCommand(string fileName, string argument)
        {
            var process = new System.Diagnostics.ProcessStartInfo
            {
                Verb = "runas",
                LoadUserProfile = true,
                FileName = fileName,
                Arguments = argument,
                RedirectStandardOutput = false,
                UseShellExecute = true,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            System.Diagnostics.Process.Start(process);
        }
        public static void EnableVirtualization() => CommandManager.RunCommand("powershell.exe", "Enable-WindowsOptionalFeature -Online -FeatureName Microsoft-Hyper-V –All");
        public static void DisableVirtualization() => CommandManager.RunCommand("powershell.exe", "Disable-WindowsOptionalFeature -Online -FeatureName Microsoft-Hyper-V-All");
        public static void Reboot()=>CommandManager.RunCommand("cmd", "/c shutdown -r -t 3");
        public static bool GetVirtualizationStatus()
        {
            Process process = Process.Start(new ProcessStartInfo
            {
                FileName = "powershell",
                Arguments = "/command Get-ComputerInfo -Property \"Hyper*\"",
                RedirectStandardOutput = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                CreateNoWindow = true
            });
            return Convert.ToBoolean(process.StandardOutput.ReadToEnd().Trim().Split(new char[] { '\r', '\n' })[0].Replace(" ", "").Split(new char[] { ':' })[1].ToLower());
        }
        //test commit
    }
}
