using XSockets.Core.Common.Socket;
using XSockets.Plugin.Framework.Core.Attributes;

namespace Azure.XSocketsServer.XSocketsNET
{
    public class XSocketsBootstrap
    {
        [ImportOne(typeof(IXBaseServerContainer))]
        public IXBaseServerContainer Wss { get; set; }

        public XSocketsBootstrap()
        {
            Wss = XSockets.Plugin.Framework.Composable.GetExport<IXBaseServerContainer>();
        }

        public void Start()
        {

            Wss.StartServers();
        }

        public void Stop()
        {
            Wss.StopServers();
        }
    }
}