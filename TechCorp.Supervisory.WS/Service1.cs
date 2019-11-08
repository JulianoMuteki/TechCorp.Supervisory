using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TechCorp.Supervisory.WS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        public string PressButtonRele(string command)
        {
            string result = SocketMicrocontroller.EnviarComandoPanda(command);
            return result;
        }

        public string GetStatusSwitch(string command)
        {
            string result = SocketMicrocontroller.EnviarComandoPanda(command);
            return result;
        }


    }
}
