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
            var processInfo = new System.Diagnostics.ProcessStartInfo
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
            System.Diagnostics.Process.Start(processInfo);
            //MessageBox.Show("enable");
            //test commit
        }

        public static void DisableVirtualization()
        {
            var processInfo = new System.Diagnostics.ProcessStartInfo
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
            System.Diagnostics.Process.Start(processInfo);
            //MessageBox.Show("disable");
        }

        public static bool GetVirtualizationStatus()
        {
            Process process2 = Process.Start(new ProcessStartInfo
            {
                FileName = "powershell",
                Arguments = "/command Get-ComputerInfo -Property \"Hyper*\"",
                RedirectStandardOutput = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                CreateNoWindow = true
            });
            return Convert.ToBoolean(process2.StandardOutput.ReadToEnd().Trim().Split(new char[] { '\r', '\n' })[0].Replace(" ", "").Split(new char[] { ':' })[1].ToLower());
        }
    }
}
