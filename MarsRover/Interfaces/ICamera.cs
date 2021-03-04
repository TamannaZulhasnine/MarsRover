using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Interfaces
{
  public interface ICamera
    {
       int Id { get; set; }
       string Name { get; set; }
       int Rover_Id { get; set; }
    }
}
