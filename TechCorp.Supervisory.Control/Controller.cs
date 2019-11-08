using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechCorp.Supervisory.Control
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
