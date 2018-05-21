using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiggerLinux.Tools
{
    public static class ShellSystem
    {
        public static string ExecCommand(string cmd, string args)
        {
            string output = "";
            try
            {
                Process proc = new Process();
                proc.StartInfo.UseShellExecute = false;                     // rediriger la sortie
                proc.StartInfo.RedirectStandardOutput = true;               // récupérer le message de sortir
                proc.StartInfo.StandardOutputEncoding = Encoding.GetEncoding("cp437");  // encodage windows Fr
                proc.StartInfo.FileName = cmd;
                proc.StartInfo.Arguments = args;
                proc.StartInfo.CreateNoWindow = true;   // cacher la console
                proc.Start();
                output += proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();
                proc.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur dans ExecCommand\r\n "
                    + "cmd : " + cmd + "\r\n args : " + args + "\r\n\r\n" + ex.ToString());
            }
            return output;
        }
    }
}
