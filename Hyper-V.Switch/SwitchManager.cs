using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hyper_V.Switch
{
    public enum RunAction
    {
        AUTO,
        OFF,
        STATUS,
        REBOOT
    }

    public class Result
    {
        public string Output { get; set; }
        public string Error { get; set; }
    }

    public class SwitchManager
    {
        public Result Run(RunAction action)
        {
            string cmdFullFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows),
                Environment.Is64BitOperatingSystem && !Environment.Is64BitProcess ? @"Sysnative\cmd.exe" : @"System32\cmd.exe");

            string args = "";

            switch (action)
            {
                case RunAction.AUTO:
                    args = @"/C bcdedit /set hypervisorlaunchtype auto";
                    break;
                case RunAction.OFF:
                    args = @"/C bcdedit /set hypervisorlaunchtype off";
                    break;
                case RunAction.STATUS:
                    args = @"/C bcdedit";
                    break;
                case RunAction.REBOOT:
                    args = @"/C shutdown /r /t 0";
                    break;
            }

            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.FileName = cmdFullFileName;
            p.StartInfo.Arguments = args;
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            string error = p.StandardError.ReadToEnd();
            p.WaitForExit();

            return new Result { Output = output, Error = error };
        }
    }
}
