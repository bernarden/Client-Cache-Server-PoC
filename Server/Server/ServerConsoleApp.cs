using System;
using System.ServiceModel;
using Server.Service;

namespace Server.App
{
    static class ServerConsoleApp
    {
        static void Main()
        {
            ServiceHost host = new ServiceHost(typeof(BasicServerService));
            try
            {
                StartService(host);
                Console.WriteLine("Press <Enter> to stop the server.");
                Console.ReadLine();
            }
            finally
            {
                host.Close();
                Console.WriteLine("WCF has been stopped.");
            }
        }

        private static void StartService(ICommunicationObject host)
        {
            Console.WriteLine("Server has been started.");

            try
            {
                Console.WriteLine("Initializing WCF.");
                host.Open();
                Console.WriteLine("WCF is running.");
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine($"An exception occurred: {ce.Message}");
            }
            finally
            {
                host.Close();
                Console.WriteLine("WCF has been stopped.");
            }
        }
    }
}