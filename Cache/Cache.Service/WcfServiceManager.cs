using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Cache.Service
{
    /// <summary>
    /// Responsible for managing wcf service
    /// </summary>
    public class WcfServiceManager : IWcfServiceManager
    {
        private static readonly Uri BaseAddress = new Uri("http://localhost:8000/CacheService");
        private readonly ServiceHost _selfHost = new ServiceHost(typeof(BasicCacheService), BaseAddress);

        /// <summary>
        /// Starts the service.
        /// </summary>
        public void StartService()
        {
            try
            {
                _selfHost.AddServiceEndpoint(typeof(ICacheService), new WSHttpBinding(), "");
                _selfHost.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });

                _selfHost.Open();
            }
            catch (CommunicationException)
            {
                _selfHost.Abort();
                throw;
            }
        }

        /// <summary>
        /// Stops the service.
        /// </summary>
        public void StopService()
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
