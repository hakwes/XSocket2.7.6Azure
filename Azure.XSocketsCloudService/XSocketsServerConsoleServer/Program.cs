using Azure.XSocketsServer;
using Azure.XSocketsServer.XSocketsNET;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XSocketsServer
{
    class Program
    {
        static void Main(string[] args)
        {

            XSocketsBootstrap host;
            host = new XSocketsBootstrap();
            host.Start();
            // Just display the known plugins (XSockets Contollers )
            foreach (var plugin in host.Wss.XSocketPlugins)
            {
                Console.WriteLine(string.Format("Plugin {0} is registred and ready", plugin.Value.Alias));
            }
            while (true)
            {
                Console.WriteLine("Current number of Connections: " + host.Wss.CurrentNumberOfConnections);
                Thread.Sleep(10000);
            }
        }
    }
}
