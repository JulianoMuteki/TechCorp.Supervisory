using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using TechCorp.Supervisory.Control;
using TechCorp.Supervisory.Control.Component;

namespace TechCorp.Supervisory.UI.Web.Models
{
    public class Fachada
    {
        internal string ButtonRelePress(string comando)
        {
            Controller controller = new Controller();
            Rele rele = new Rele();

            ICommand typeCommand = null;

            if (comando == "ON")
            {
                //typeCommand how releOn
                typeCommand = new ReleOnCommand(rele);
                controller.setCommand(typeCommand);
            }
            else
            {
                //typeCommand how releOff
                typeCommand = new ReleOffCommand(rele);
                controller.setCommand(typeCommand);
            }
            string retorno = controller.SendCommand(0);
            return retorno;
        }

        internal string CheckSwitch(int numberSwitch)
        {
            Controller controller = new Controller();
            Switch swithClass = new Switch();

            ICommand typeCommand = null;

            //typeCommand how SwitchGetStateCommand
            typeCommand = new SwitchGetStateCommand(swithClass);
            controller.setCommand(typeCommand);

            string retorno = controller.SendCommand(0);

            return retorno;
        }
    }
}