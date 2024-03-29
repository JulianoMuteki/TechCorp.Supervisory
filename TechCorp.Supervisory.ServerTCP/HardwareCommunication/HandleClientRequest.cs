﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace TechCorp.Supervisory.ServerTCP.HardwareCommunication
{
    public class HandleClientRequest
    {
        public event ComandoHardwareEvent ComandoHardware;
        public delegate void ComandoHardwareEvent(string comando);

        TcpClient _clientSocket;
        NetworkStream _networkStream = null;
        public HandleClientRequest(TcpClient clientConnected)
        {
            this._clientSocket = clientConnected;
        }
        public void StartClient()
        {
            _networkStream = _clientSocket.GetStream();
            WaitForRequest();
        }

        public void WaitForRequest()
        {
            byte[] buffer = new byte[_clientSocket.ReceiveBufferSize];

            _networkStream.BeginRead(buffer, 0, buffer.Length, ReadCallback, buffer);
        }

        private void ReadCallback(IAsyncResult result)
        {
            NetworkStream networkStream = _clientSocket.GetStream();
            try
            {
                int read = networkStream.EndRead(result);
                if (read == 0)
                {
                    _networkStream.Close();
                    _clientSocket.Close();
                    return;
                }

                byte[] buffer = result.AsyncState as byte[];
                string data = Encoding.Default.GetString(buffer, 0, read);
                Console.WriteLine(data);
                //do the job with the data here
                //send the data back to client.
                Byte[] sendBytes = Encoding.ASCII.GetBytes("Server write: " + DateTime.Now.ToString());
                networkStream.Write(sendBytes, 0, sendBytes.Length);
                networkStream.Flush();
            }
            catch (Exception ex)
            {
                throw;
            }

            this.WaitForRequest();
        }

        

    }
}
