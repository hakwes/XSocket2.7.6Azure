using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using System.Reflection;
using System.IO;
using HtmlHelpers;

namespace Azure.XSocketsClientWorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        Process xsocketsClient;

        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.TraceInformation("Azure.SimulatorWorker entry point called", "Information");

            //Start XSocketsClient in new Process.            
            xsocketsClient = new Process();

            ProcessStartInfo startInfo = new ProcessStartInfo();
            string path = Assembly.GetExecutingAssembly().Location;

            startInfo.WorkingDirectory = Path.GetDirectoryName(path);
            startInfo.FileName = "Azure.XSocketsConsoleClient.exe";

            Trace.TraceInformation("Starting: " + Path.Combine(startInfo.WorkingDirectory, startInfo.FileName));

            xsocketsClient.StartInfo = startInfo;
            xsocketsClient.Start();
           // xsocketsClient.Attach(); // Attach to the visual studio debugger using extension method

            while (true)
            {
                Thread.Sleep(10000);
                //Trace.TraceInformation("Working", "Information");
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }
    }
}
