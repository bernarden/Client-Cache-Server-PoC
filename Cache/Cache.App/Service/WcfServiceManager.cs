using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Cache.WPF.ViewModels;

namespace Cache.WPF.Service
{
    /// <summary>
    /// Responsible for managing wcf service
    /// </summary>
    public static class WcfServiceManager
    {
        private static ServiceHost _selfHost;

        /// <summary>
        /// Starts the service.
        /// </summary>
        public static void StartService()
        {
            try
            {
                if (_selfHost == null)
                {
                    _selfHost = InitializeService();
                }
                _selfHost.AddServiceEndpoint(typeof(ICacheService), new WSHttpBinding(), "");
                _selfHost.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });
                _selfHost.Open();
            }
            catch (CommunicationException)
            {
                _selfHost?.Abort();
                throw;
            }
        }

        private static ServiceHost InitializeService()
        {

            var baseAddress = new Uri("http://localhost:808/CacheService");
 


            return new ServiceHost(typeof(BasicCacheService), baseAddress);
        }

        /// <summary>
        /// Stops the service.
        /// </summary>
        public static void StopService()
        {
            try
            {
                if (_selfHost.State == CommunicationState.Created || _selfHost.State == CommunicationState.Faulted ||
                    _selfHost.State == CommunicationState.Opened || _selfHost.State == CommunicationState.Opening)
                {
                    _selfHost.Close();
                }
            }
            catch (CommunicationException)
            {
                _selfHost.Abort();
                throw;
            }
        }
    }
}
