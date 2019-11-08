using System;
using Microsoft.SPOT;

namespace TechCorp.Supervisory.ControlFEZPanda.Component
{
    public class ReleOnCommand : ICommand
    {
        Rele _rele;

        public ReleOnCommand(Rele rele)
        {
            this._rele = rele;
        }

        public string Execute(int part)
        {
            return _rele.LigarRele(part);
        }
    }
}
