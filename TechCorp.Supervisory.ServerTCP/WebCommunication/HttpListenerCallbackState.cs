using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;

namespace TechCorp.Supervisory.ServerTCP.WebCommunication
{
    public class HttpListenerCallbackState
    {
        private readonly HttpListener _listener;
        private readonly AutoResetEvent _listenForNextRequest;

        public HttpListenerCallbackState(HttpListener listener)
        {
            if (listener == null) throw new ArgumentNullException("listener");
            _listener = listener;
            _listenForNextRequest = new AutoResetEvent(false);
        }

        public HttpListener Listener { get { return _listener; } }
        public AutoResetEvent ListenForNextRequest { get { return _listenForNextRequest; } }
    }
}
