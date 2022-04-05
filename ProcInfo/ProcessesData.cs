using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace ProcInfo
{
    internal static class ProcessesData
    {
        public static string GetIDsString()
        {
            string target="";
            var procs=Process.GetProcesses();
            foreach(var proc in procs)
            {
                target +=proc.Id+"*";
            }
            return target;
        }
        public static int[] GetIDsInt()
        {
            var procs = Process.GetProcesses();
            int[] target =new int[procs.Length];
            int i = 0;
            foreach (var proc in procs)
            {
                target[i] = proc.Id;
                i++;
            }
            return target;
        }
        public static List<Process> GetProcessList()
        {
            try
            {
                return Process.GetProcesses().ToList();
            }
            catch (ArgumentNullException)
            {
                return new List<Process>();
            }
        }
        public static List<string> GetProcessStringList()
        {
            List<string> target = new List<string>();
            foreach (Process proc in Process.GetProcesses())
            {
                target.Add(proc.Id + "*" + proc.ProcessName);
            }
            return target;
        }
    }
}
