using System;
using Microsoft.SPOT;

namespace TechCorp.Supervisory.ControlFEZPanda
{
    public class Controller
    {
        ICommand _command;

        public Controller()
        {

        }

        public void setCommand(ICommand command)
        {
            _command = command;
        }

        public string SendCommand(int part)
        {
            return _command.Execute(part);
        }
    }
}
