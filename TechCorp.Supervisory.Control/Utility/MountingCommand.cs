using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechCorp.Supervisory.Control.Utility
{
   public static class MountingCommand
    {
       public static string MountCommand(string command, string parameter, int part)
       {
           System.Text.StringBuilder sb = new System.Text.StringBuilder();

           sb.Append(command);
           sb.Append("=");
           sb.Append(parameter);
           sb.Append("|");
           sb.Append(part);
           return sb.ToString();
       }
    }
}
