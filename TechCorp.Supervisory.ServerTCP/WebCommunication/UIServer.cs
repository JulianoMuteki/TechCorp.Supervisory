using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechCorp.Supervisory.ServerTCP.WebCommunication
{
   public class UIServer
    {
       public void StartCommunicationWithUI()
       {
           HttpRequestHandler _httpRequest = new HttpRequestHandler();
           _httpRequest.ListenAsynchronously(new List<string>() { "http://192.168.1.200:8000/" });
       }
    }
}
