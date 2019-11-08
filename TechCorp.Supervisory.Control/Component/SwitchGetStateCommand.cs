using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechCorp.Supervisory.Control.Component
{
   public class SwitchGetStateCommand: ICommand
    {
       Switch _switch;
       public SwitchGetStateCommand(Switch switchClass)
       {
           _switch = switchClass;
       }
        public string Execute(int part)
        {
          return  _switch.GetState(part);
        }
    }
}
