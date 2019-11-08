using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechCorp.Supervisory.Control.Component
{
    public class Rele
    {
        private const string nameClass = NetFrameworkCommands.TypesClass.Rele;

        public string On(int part)
        {
            string parameter = NetFrameworkCommands.TypeCommand.On;

            string command = Utility.MountingCommand.MountCommand(nameClass, parameter, part);
            using (Supervisory.WS.Service1Client service = new Supervisory.WS.Service1Client())
            {
                string result = service.PressButtonRele(command);
                return result;
            }
        }

        public string Off(int part)
        {
            string parameter = NetFrameworkCommands.TypeCommand.Off;

            string command = Utility.MountingCommand.MountCommand(nameClass, parameter, part);
            using (Supervisory.WS.Service1Client service = new Supervisory.WS.Service1Client())
            {
                string result = service.PressButtonRele(command);
                return result;
            }
        }
    }
}
