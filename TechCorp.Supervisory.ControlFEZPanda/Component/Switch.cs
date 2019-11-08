using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.FEZ;

namespace TechCorp.Supervisory.ControlFEZPanda.Component
{
    public class Switch
    {
        private const string nameClass = "SWITCH_STATE";

        private static InputPort Button = new InputPort((Cpu.Pin)FEZ_Pin.Digital.LDR, false,
  Port.ResistorMode.PullUp);

        public string GetState(int numberSwitch)
        {
            //Button is false when pressed
            string stateReturn = string.Empty;
            if (Button.Read())
                stateReturn = "OFF";
            else
                stateReturn = "ON";

            return stateReturn;
        }
    }
}
