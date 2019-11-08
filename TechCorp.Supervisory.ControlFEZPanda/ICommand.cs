using System;
using Microsoft.SPOT;

namespace TechCorp.Supervisory.ControlFEZPanda
{
    public interface ICommand
    {
        string Execute(int part);
    }
}
