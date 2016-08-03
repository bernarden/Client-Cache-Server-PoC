using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Description;
using Server.Common;
using Server.Core;
using ServerService;

namespace Server
{
    static class ServerConsoleApp
    {
        static void Main()
        {
            string originalFilePath = Path.Combine(CommonConstants.ServerFilesLocation, "file.txt");
            string modifiedFilePath = Path.Combine(CommonConstants.ServerFilesLocation, "file - Copy.txt");
            string originalString;
            string modifiedString;

            using (StreamReader r = new StreamReader(originalFilePath))
            {
                originalString = r.ReadToEnd();
            }
            using (StreamReader r = new StreamReader(modifiedFilePath))
            {
                modifiedString = r.ReadToEnd();
            }

            List<Chunk> chunksFromOriginalFile = RabinKarpAlgorithm.Slice(originalString);

            List<Chunk> chunksFromModifiedFile = RabinKarpAlgorithm.Slice(modifiedString);

            Console.WriteLine($"Chunks in file '{new FileInfo(originalFilePath).Name}'\t: {chunksFromOriginalFile.Count}");
            Console.WriteLine($"Chunks in file '{new FileInfo(modifiedFilePath).Name}'\t: {chunksFromModifiedFile.Count}");


            ChunkDifferentiator.GetUpdatedChunks(chunksFromOriginalFile, chunksFromModifiedFile);
            //StartService();
            Console.ReadLine();
        }

        private static void StartService()
        {
            Console.WriteLine("Server has been started.");

            Uri baseAddress = new Uri("http://localhost:8000/BasicServerService/");
            ServiceHost selfHost = new ServiceHost(typeof(BasicServerService), baseAddress);

            try
            {
                selfHost.AddServiceEndpoint(typeof(IServerService), new WSHttpBinding(), "BasicServerService");
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior { HttpGetEnabled = true };
                selfHost.Description.Behaviors.Add(smb);
                selfHost.Open();
                Console.WriteLine("Server is operational.");
                Console.WriteLine("Press Enter to shut it down.\n");

                Console.ReadLine();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                selfHost.Abort();
                Console.ReadLine();
            }
            finally
            {
                selfHost.Close();
            }
        }
    }
}