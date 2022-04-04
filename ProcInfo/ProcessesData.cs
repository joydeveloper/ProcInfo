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
    }
}
