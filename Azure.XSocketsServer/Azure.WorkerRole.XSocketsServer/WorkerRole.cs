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
using Azure.XSocketsServer;
using Azure.XSocketsServer.XSocketsNET;

namespace Azure.XSocketServer
{
    public class WorkerRole : RoleEntryPoint
    {

        XSocketsBootstrap host;
        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.TraceInformation("Azure.XSocketServer entry point called", "Information");
            host = new XSocketsBootstrap();

            host.Start();

            host.Wss.OnError += Wss_OnError;

            // Just display the known plugins (XSockets Contollers )
            foreach (var plugin in host.Wss.XSocketPlugins)
            {
                Trace.TraceInformation(string.Format("Plugin {0} is registred and ready", plugin.Value.Alias));
            }
            while (true)
            {
                Trace.TraceInformation("Current number of Connections: " + host.Wss.CurrentNumberOfConnections);
                Thread.Sleep(10000);
            }


        }

        void Wss_OnError(object sender, XSockets.Core.Common.Socket.Event.Arguments.OnErrorArgs e)
        {
            Trace.TraceError("Error" + e.ToString());
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
