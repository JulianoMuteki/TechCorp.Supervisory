using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.FEZ;

namespace TechCorp.Supervisory.ControlFEZPanda.Component
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
