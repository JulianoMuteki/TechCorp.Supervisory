using System;
using Microsoft.SPOT;

namespace TechCorp.Supervisory.ControlFEZPanda.Component
{
    public class ReleOffCommand : ICommand
    {
        Rele _rele;
        public ReleOffCommand(Rele rele)
        {
            this._rele = rele;
        }
        public string Execute(int part)
        {
            return _rele.DesligarRele(part);
        }
    }
}
