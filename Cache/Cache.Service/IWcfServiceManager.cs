namespace Cache.Service
{
    public interface IWcfServiceManager
    {
        /// <summary>
        /// Starts the service.
        /// </summary>
        void StartService();

        /// <summary>
        /// Stops the service.
        /// </summary>
        void StopService();
    }
}