using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace TechCorp.Supervisory.ServerTCP.HardwareCommunication
{
  public  class HardwareServer
    {

      public event RecebendoComandoHardwareEvent RecebendoComandoHardware;
      public delegate void RecebendoComandoHardwareEvent(string comando);

        private TcpListener _listener;
        public void StartServer()
        {
            System.Net.IPAddress localIPAddress = System.Net.IPAddress.Parse("192.168.1.200");
            IPEndPoint ipLocal = new IPEndPoint(localIPAddress, 2000);
            _listener = new TcpListener(ipLocal);
            _listener.Start();
            WaitForClientConnect();
        }
        private void WaitForClientConnect()
        {
            object obj = new object();
            _listener.BeginAcceptTcpClient(new System.AsyncCallback(OnClientConnect), obj);
        }
        private void OnClientConnect(IAsyncResult asyn)
        {
            try
            {
                TcpClient clientSocket = default(TcpClient);
                clientSocket = _listener.EndAcceptTcpClient(asyn);
                HandleClientRequest clientReq = new HandleClientRequest(clientSocket);
                clientReq.ComandoHardware += new HandleClientRequest.ComandoHardwareEvent(clientReq_ComandoHardware);
                clientReq.StartClient();
            }
            catch (Exception se)
            {
                throw;
            }

            WaitForClientConnect();
        }

        void clientReq_ComandoHardware(string comando)
        {
            RecebendoComandoHardware(comando);
        }



        internal void ExecutarComando(string comando)
        {
           
            throw new NotImplementedException();
        }
    }
}
