using System;
using System.Collections.Generic;
using System.Text;
using MarsRover.Models;

namespace MarsRover.Interfaces
{
   public interface IPhoto
    {
       int Id { get; set; }
       Camera Camera { get; set; }
       Rover Rover { get; set; }
    }
}
