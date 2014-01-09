using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using EnvDTE;

namespace HtmlHelpers
{
    /// <summary>
    /// Class ProcessExtender
    /// Attach to other processes in order to debug.
    /// Code by: Niel Morgan Thomas
    /// License: CPOL
    /// </summary>
    public static class ProcessExtender
    {
        const string progId = @"VisualStudio.DTE.12.0";

        /// <summary>
        /// Attaches Visual Studio (2010) to the specified process.
        /// </summary>
        /// <param name="process">The process.</param>
        public static void Attach(this System.Diagnostics.Process process)
        {
            // Reference visual studio core
            DTE dte;
            try
            {
                dte = (DTE)Marshal.GetActiveObject(progId);
            }
            catch (COMException)
            {
                Debug.WriteLine(String.Format(@"Visual studio not found."));
                return;
            }

            // Try loop - visual studio may not respond the first time.
            int tryCount = 10;
            while (tryCount-- > 0)
            {
                try
                {
                    Processes processes = dte.Debugger.LocalProcesses;
                    foreach (EnvDTE.Process proc in processes.Cast<EnvDTE.Process>().Where(
                      proc => proc.Name.IndexOf(process.ProcessName) != -1))
                    {
                        proc.Attach();
                        Debug.WriteLine(String.Format("Attatched to process {0} successfully.", process.ProcessName));
                        break;
                    }
                    break;
                }
                catch (COMException)
                {
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
    }
}

