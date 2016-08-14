using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Cache.ServerClient;

namespace Cache.Service
{
    /// <summary>
    /// Responsible for managing wcf service
    /// </summary>
    public class WcfServiceManager : IWcfServiceManager, IDisposable
    {
        readonly ServiceHost _selfHost;

        public WcfServiceManager()
        {
            var baseAddress = new Uri("http://localhost:808/CacheService");
            _selfHost = new ServiceHost(new BasicCacheService(new ServerFileClient()), baseAddress);
        }

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

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                StopService();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
