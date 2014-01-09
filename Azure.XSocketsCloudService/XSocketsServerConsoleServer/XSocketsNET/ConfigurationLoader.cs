using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using XSockets.Core.Common.Configuration;
using XSockets.Core.Configuration;
using XSockets.Plugin.Framework.Core.Attributes;
namespace Azure.XSocketsServer.XSocketsNET
{
    [Export(typeof(IConfigurationLoader))]
    public class ConfigurationLoader : IConfigurationLoader
    {
        public IConfigurationSettings Settings = null;

        public Uri GetUri(string url)
        {
            try
            {
                return new Uri(url);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public HashSet<string> GetOrigins()
        {
            //var origins = Microsoft.WindowsAzure.CloudConfigurationManager.GetSetting("XSocketsOrigins");
            var origins = RoleEnvironment.GetConfigurationSettingValue("XSocketsOrigins");
            return new HashSet<string>(!string.IsNullOrEmpty(origins) ? origins.Split(',').ToList() : new List<string> { "*" });

        }

        public IConfigurationSettings ConfigurationSettings
        {
            get
            {
                if (Settings == null)
                {
                    var uri = GetUri(RoleEnvironment.GetConfigurationSettingValue("XSocketsHostUri"));

                    Settings = new ConfigurationSettings
                    {
                        Endpoint = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["XSocketsEndpoint"].IPEndpoint,
                        Port = uri.Port,
                        Origin = GetOrigins(),
                        Location = uri.Host,
                        Scheme = uri.Scheme,
                        Uri = uri,
                        BufferSize = 8192,
                        //RemoveInactiveStorageAfterXDays = 7,
                        //RemoveInactiveChannelsAfterXMinutes = 30,
                        NumberOfAllowedConections = -1
                    };
                }
                return Settings;
            }
        }
    }
}
