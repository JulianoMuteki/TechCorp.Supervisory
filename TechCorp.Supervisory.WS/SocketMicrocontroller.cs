using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace TechCorp.Supervisory.WS
{
    //Socket Client
   public static class SocketMicrocontroller
    {
       public static string EnviarComandoPanda(string comando)
       {
           Console.WriteLine("\r\nMicrocontroller");
           string mensagemRetorno = "";
           int count = 0;
           try
           {
               using (Socket mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
               {
                   IPAddress ServerIP = new IPAddress(new byte[] { 192, 168, 1, 100 });
                   IPEndPoint ServerEndPoint = new IPEndPoint(ServerIP, 2000);
                   try
                   {

                       mySocket.Connect(ServerEndPoint);

                       bool flagFinalize = false;
                       //end the command only when sending all data
                       while (!flagFinalize)
                       {
                           byte[] buf = Encoding.UTF8.GetBytes(comando);
                           int bytesSent = mySocket.Send(buf);

                           Console.WriteLine("Send data from Microcontroller: " + comando);

                           if (bytesSent == buf.Count() || count == 10)//ends when all sent or attempted equals 10
                           {
                               flagFinalize = true;
                           }
                           count++;
                       }

                       if (count != 10)//case sent some data
                       {
                           count = 0;
                           System.Threading.Thread.Sleep(1000);
                           while (mySocket.Poll(200000, SelectMode.SelectRead))
                           {
                               if (count == 20)//attempt to receive data more than once
                                   break;

                               if (mySocket.Available > 0)
                               {
                                   byte[] inBuf = new byte[mySocket.Available];
                                   EndPoint recEndPoint = new IPEndPoint(IPAddress.Any, 0);
                                   mySocket.ReceiveFrom(inBuf, ref recEndPoint);
                                   mensagemRetorno = new string(Encoding.UTF8.GetChars(inBuf));
                                   Console.WriteLine("Receive data from Microcontroller: " + mensagemRetorno);
                                   break;
                               }
                               count++;
                           }
                       }
                   }
                   catch (ArgumentNullException ane)
                   {
                       Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                       mensagemRetorno = ane.Message;
                   }
                   catch (SocketException se)
                   {
                       Console.WriteLine("SocketException : {0}", se.ToString());
                       mensagemRetorno = se.Message;
                   }
                   catch (Exception e)
                   {
                       Console.WriteLine("Unexpected exception : {0}", e.ToString());
                       mensagemRetorno = e.Message;
                   }
               }
           }
           catch
           { }

           return mensagemRetorno;

       }

    }
}
