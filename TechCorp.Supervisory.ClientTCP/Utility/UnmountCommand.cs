using System;
using Microsoft.SPOT;

namespace TechCorp.Supervisory.ClientTCP.Utility
{
  public static  class UnmountCommand
    {
      public static string[] Unmount(string command)
      {
          string[] words = command.Split(new Char [] {'=','|'});

          return words;
      }
    }
}
