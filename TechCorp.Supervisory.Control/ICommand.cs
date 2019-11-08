using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechCorp.Supervisory.Control
{
   public interface ICommand
    {
        string Execute(int part);
    }
}
