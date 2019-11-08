using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechCorp.Supervisory.Control.Component
{
    public class Switch
    {
        private const string nameClass = NetFrameworkCommands.TypesClass.Switch ;

        public string GetState(int numberSwitch)
        {
            string parameter = NetFrameworkCommands.TypeCommand.Get_State;

            string resultCommand = Utility.MountingCommand.MountCommand(nameClass, parameter, numberSwitch);
            using (Supervisory.WS.Service1Client service = new Supervisory.WS.Service1Client())
            {
                string result = service.GetStatusSwitch(resultCommand);
                return result;
            }
        }
    }
}
