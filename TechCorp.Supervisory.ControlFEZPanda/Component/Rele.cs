using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.FEZ;

namespace TechCorp.Supervisory.ControlFEZPanda.Component
{
    public class Rele
    {
        static OutputPort MyLED = new OutputPort((Cpu.Pin)FEZ_Pin.Digital.LED, false);

        public string LigarRele(int part)
        {           
            MyLED.Write(true);
            return "OK";
        }

        public string DesligarRele(int part)
        {
            MyLED.Write(false);
            return "OK";
        }
    }
}
