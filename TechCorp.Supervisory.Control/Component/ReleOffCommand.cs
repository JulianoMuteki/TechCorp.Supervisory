﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechCorp.Supervisory.Control.Component;

namespace TechCorp.Supervisory.Control.Component
{
   public class ReleOffCommand: ICommand
    {
       Rele _rele;
       public ReleOffCommand(Rele rele)
       {
           this._rele = rele;
       }
        public string Execute(int part)
        {
            return _rele.Off(part);
        }
    }
}
