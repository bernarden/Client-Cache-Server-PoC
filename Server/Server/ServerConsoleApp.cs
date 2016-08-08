using System;
using System.ServiceModel;
using Server.Service;

namespace Server.App
{
    static class ServerConsoleApp
    {
        static void Main()
        {
            Console.WriteLine("Server has been started.");
            ServiceHost host = new ServiceHost(typeof(BasicServerService));
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
    }
}