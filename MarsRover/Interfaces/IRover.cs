using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Models
{
   public interface IRover
    {
       int Id { get; set; }
       string Name { get; set; }
    }
}
