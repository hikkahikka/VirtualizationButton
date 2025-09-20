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
        public static void EnableVirtualization()
        {
            var process = new System.Diagnostics.ProcessStartInfo
            {
                Verb = "runas",
                LoadUserProfile = true,
                FileName = "powershell.exe",
                Arguments = "Enable-WindowsOptionalFeature -Online -FeatureName Microsoft-Hyper-V –All",
                RedirectStandardOutput = false,
                UseShellExecute = true,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            System.Diagnostics.Process.Start(process);
        }

        public static void DisableVirtualization()
        {
            var process = new System.Diagnostics.ProcessStartInfo
            {
                Verb = "runas",
                LoadUserProfile = true,
                FileName = "powershell.exe",
                Arguments = "Disable-WindowsOptionalFeature -Online -FeatureName Microsoft-Hyper-V-All",
                RedirectStandardOutput = false,
                UseShellExecute = true,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            System.Diagnostics.Process.Start(process);
        }

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

        public static void Reboot()
        {
            Process process = Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = "/c shutdown -r -t 5",
                RedirectStandardOutput = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                CreateNoWindow = true
            });
        }
    }
}
