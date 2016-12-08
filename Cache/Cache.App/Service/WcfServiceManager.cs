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
                var wsHttpBinding = new WSHttpBinding
                {
                    MaxBufferPoolSize = 2147483647,
                    MaxReceivedMessageSize = 2147483647
                };


#if DEBUG
                wsHttpBinding.ReceiveTimeout = new TimeSpan(0, 10, 0);
                wsHttpBinding.SendTimeout = new TimeSpan(0, 10, 0);
                wsHttpBinding.OpenTimeout = new TimeSpan(0, 10, 0);
                wsHttpBinding.CloseTimeout = new TimeSpan(0, 10, 0);

                _selfHost.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true, HttpsGetEnabled = true });
                _selfHost.Description.Behaviors.RemoveAll<ServiceDebugBehavior>();
                _selfHost.Description.Behaviors.Add(new ServiceDebugBehavior { IncludeExceptionDetailInFaults = true });
#endif
                
                _selfHost.AddServiceEndpoint(typeof(ICacheService), wsHttpBinding, "");
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
