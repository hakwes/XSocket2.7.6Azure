using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XSockets.Core.Common.Socket.Event.Interface;
using XSockets.External;

namespace XSocketsConsoleClient
{
    class Program
    {

        static void Main(string[] args)
        {
            Thread.Sleep(10000); //To allow the console to be attached to the visual studio debugger. 
            var ws = new XWebSocket(@"ws://127.0.0.1:4502/Generic", "http://*", false);
            try
            {
                ws.OnOpen += (s, e) =>
                {
                    Console.WriteLine("Connected");
                };


                ws.OnError += (s, e) =>
                {
                    Console.WriteLine("Error: " + e.ToString());
                };

                
                ws.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(" - Exception connecting to XSockets: " + ex.ToString());
            }

            while (true)
            {
                Thread.Sleep(10000);

            }
        }
    }
}
