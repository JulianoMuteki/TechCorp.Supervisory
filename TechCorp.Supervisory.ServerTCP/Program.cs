using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TechCorp.Supervisory.ServerTCP.HardwareCommunication;
using TechCorp.Supervisory.ServerTCP.WebCommunication;

namespace TechCorp.Supervisory.ServerTCP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Server");
            var uiServer = new UIServer();
            try
            {
               
                Thread aThread = new Thread(new ThreadStart(uiServer.StartCommunicationWithUI));
                aThread.Start();
            }
            catch { }
            Console.WriteLine("Started Server");
            System.Threading.Thread.Sleep(Timeout.Infinite);
        }
    }




}
