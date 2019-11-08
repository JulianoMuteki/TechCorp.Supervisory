using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.Net.Sockets;

namespace TechCorp.Supervisory.ServerTCP.WebCommunication
{
    public class HttpRequestHandler
    {
        private int requestCounter = 0;
        private ManualResetEvent stopEvent = new ManualResetEvent(false);

        public void ListenAsynchronously(IEnumerable<string> prefixes)
        {
            try
            {
                using (HttpListener listener = new HttpListener())
                {
                    foreach (string s in prefixes)
                    {
                        listener.Prefixes.Add(s);
                    }

                    listener.Start();
                    HttpListenerCallbackState state = new HttpListenerCallbackState(listener);
                    ThreadPool.QueueUserWorkItem(Listen, state);
                }
            }
            catch
            { }
        }

        public void StopListening()
        {
            stopEvent.Set();
        }

        private void Listen(object state)
        {
            HttpListenerCallbackState callbackState = (HttpListenerCallbackState)state;

            while (callbackState.Listener.IsListening)
            {
                callbackState.Listener.BeginGetContext(new AsyncCallback(ListenerCallback), callbackState);
                int n = WaitHandle.WaitAny(new WaitHandle[] { callbackState.ListenForNextRequest, stopEvent });

                if (n == 1)
                {
                    // stopEvent was signalled 
                    callbackState.Listener.Stop();
                    break;
                }
            }
        }

        private void ListenerCallback(IAsyncResult ar)
        {
            HttpListenerCallbackState callbackState = (HttpListenerCallbackState)ar.AsyncState;
            HttpListenerContext context = null;

            int requestNumber = Interlocked.Increment(ref requestCounter);

            try
            {
                context = callbackState.Listener.EndGetContext(ar);
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {
                callbackState.ListenForNextRequest.Set();
            }

            if (context == null) return;


            HttpListenerRequest request = context.Request;
            Console.WriteLine("\r\nServer Request");
            string responseMicrocontroller = "";

            if (request.HasEntityBody)
            {
                System.IO.Stream body = request.InputStream;
                System.Text.Encoding encoding = request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                if (request.ContentType != null)
                {
                    Console.WriteLine("Client data content type {0}", request.ContentType);
                }

                string s = reader.ReadToEnd();

                responseMicrocontroller = EnviarComandoPanda(s);

                body.Close();
                reader.Close();
            }

            if (string.IsNullOrEmpty(responseMicrocontroller))
                responseMicrocontroller = "NO";

            try
            {
                using (HttpListenerResponse response = context.Response)
                {
                    //response stuff happens here  
                    Console.WriteLine("Send data from web: " + responseMicrocontroller);
                    byte[] buffer = Encoding.UTF8.GetBytes(responseMicrocontroller);
                    response.ContentLength64 = buffer.LongLength;
                    response.OutputStream.Write(buffer, 0, buffer.Length);
                    context.Response.OutputStream.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            { 
                context.Response.Close(); 
            }
            catch { }
        }

        private string EnviarComandoPanda(string comando)
        {
            Console.WriteLine("\r\nMicrocontroller");
            string mensagemRetorno = "";
            int count = 0;

            using (Socket mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                IPAddress ServerIP = new IPAddress(new byte[] { 192, 168, 1, 100 });
                IPEndPoint ServerEndPoint = new IPEndPoint(ServerIP, 2000);
                // mySocket.Bind(ServerEndPoint);
                mySocket.Connect(ServerEndPoint);

                bool flagFinaliza = false;
                while (!flagFinaliza)
                {
                    byte[] buf = Encoding.UTF8.GetBytes(comando);
                    int bytesSent = mySocket.Send(buf);

                    Console.WriteLine("Send data from Microcontroller: " + comando);

                    if (bytesSent == buf.Count() || count == 10)
                    {
                        flagFinaliza = true;
                    }
                    count++;
                }

                if (count != 10)
                {
                    count = 0;
                    System.Threading.Thread.Sleep(1000);
                    while (mySocket.Poll(200000, SelectMode.SelectRead))
                    {
                        if (count == 20)
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

                mySocket.Disconnect(false);
                return mensagemRetorno;
            }
        }


    }

    //public class HttpRequestHandler
    //{
    //    public void ListenAsynchronously(IEnumerable<string> prefixes)
    //    {
    //        HttpListener listener = new HttpListener();
    //        foreach (string s in prefixes)
    //        {
    //            listener.Prefixes.Add(s);
    //        }
    //        listener.Start();
    //        IAsyncResult result = listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);
    //        // Applications can do some work here while waiting for the  
    //        // request. If no work can be done until you have processed a request, 
    //        // use a wait handle to prevent this thread from terminating 
    //        // while the asynchronous operation completes.
    //        Console.WriteLine("Waiting for request to be processed asyncronously.");
    //        result.AsyncWaitHandle.WaitOne();
    //        Console.WriteLine("Request processed asyncronously.");
    //        listener.Close();
    //    }

    //    public void ListenerCallback(IAsyncResult result)
    //    {
    //        HttpListener listener = (HttpListener)result.AsyncState;
    //        // Call EndGetContext to complete the asynchronous operation.
    //        HttpListenerContext context = listener.EndGetContext(result);
    //        HttpListenerRequest request = context.Request;
    //        // Construct a response.   

    //        Console.WriteLine("\r\nServer Request");
    //        string responseMicrocontroller = "";

    //        if (request.HasEntityBody)
    //        {
    //            System.IO.Stream body = request.InputStream;
    //            System.Text.Encoding encoding = request.ContentEncoding;
    //            System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
    //            if (request.ContentType != null)
    //            {
    //                Console.WriteLine("Client data content type {0}", request.ContentType);
    //            }

    //            string s = reader.ReadToEnd();

    //            responseMicrocontroller = EnviarComandoPanda(s);

    //            body.Close();
    //            reader.Close();
    //        }

    //        if (!string.IsNullOrEmpty(responseMicrocontroller))
    //        {
    //            try
    //            {
    //                using (HttpListenerResponse response = context.Response)
    //                {
    //                    //response stuff happens here  
    //                    Console.WriteLine("Send data from web: " + responseMicrocontroller);
    //                    byte[] buffer = Encoding.UTF8.GetBytes(responseMicrocontroller);
    //                    response.ContentLength64 = buffer.LongLength;
    //                    response.OutputStream.Write(buffer, 0, buffer.Length);
    //                    response.Close();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                Console.WriteLine(ex.Message);
    //            }
    //        }

    //        try
    //        {
    //            context.Response.OutputStream.Close();
    //        }
    //        catch
    //        { }
    //    }

    //    private string EnviarComandoPanda(string comando)
    //    {
    //        Console.WriteLine("\r\nMicrocontroller");
    //        string mensagemRetorno = "";
    //        int count = 0;

    //        using (Socket mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
    //        {
    //            IPAddress ServerIP = new IPAddress(new byte[] { 192, 168, 1, 100 });
    //            IPEndPoint ServerEndPoint = new IPEndPoint(ServerIP, 2000);
    //            // mySocket.Bind(ServerEndPoint);
    //            mySocket.Connect(ServerEndPoint);

    //            bool flagFinaliza = false;
    //            while (!flagFinaliza)
    //            {
    //                byte[] buf = Encoding.UTF8.GetBytes(comando);
    //                int bytesSent = mySocket.Send(buf);

    //                Console.WriteLine("Send data from Microcontroller: " + comando);

    //                if (bytesSent == buf.Count() || count == 10)
    //                {
    //                    flagFinaliza = true;
    //                }
    //                count++;
    //            }

    //            if (count != 10)
    //            {
    //                count = 0;
    //                System.Threading.Thread.Sleep(1000);
    //                while (mySocket.Poll(200000, SelectMode.SelectRead))
    //                {
    //                    if (count == 20)
    //                        break;

    //                    if (mySocket.Available > 0)
    //                    {
    //                        byte[] inBuf = new byte[mySocket.Available];
    //                        EndPoint recEndPoint = new IPEndPoint(IPAddress.Any, 0);
    //                        mySocket.ReceiveFrom(inBuf, ref recEndPoint);
    //                        mensagemRetorno = new string(Encoding.UTF8.GetChars(inBuf));
    //                        Console.WriteLine("Receive data from Microcontroller: " + mensagemRetorno);
    //                        break;
    //                    }
    //                    count++;
    //                }
    //            }

    //            mySocket.Disconnect(false);
    //            return mensagemRetorno;
    //        }
    //    }
    //}
}
