using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Server.Service;

namespace Server.App
{
    public static class ServerConsoleApp
    {
        static void Main()
        {

            Console.WriteLine("Server has been started.");
            var host = SetupServiceHost();

            try
            {
                Console.WriteLine("Initializing WCF.");
                host.Open();
                Console.WriteLine("WCF has been started.");
                Console.WriteLine("Press <Enter> to stop the server.");
                Console.ReadLine();
                host.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"An exception occurred: {e.Message}");
                host.Abort();
                Console.WriteLine("WCF has been aborted.");
            }
            Console.ReadLine();
        }

        private static ServiceHost SetupServiceHost()
        {
            var baseAddress = new Uri("http://localhost:808/ServerService");
            ServiceHost host = new ServiceHost(typeof(BasicServerService), baseAddress);

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

            host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true, HttpsGetEnabled = true });
            host.Description.Behaviors.RemoveAll<ServiceDebugBehavior>();
            host.Description.Behaviors.Add(new ServiceDebugBehavior { IncludeExceptionDetailInFaults = true });
#endif

            host.AddServiceEndpoint(typeof(IServerService), wsHttpBinding, "");
            return host;
        }
    }
}