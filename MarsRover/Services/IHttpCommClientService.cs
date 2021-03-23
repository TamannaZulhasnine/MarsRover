using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Interfaces
{
   public interface IHttpCommClientService
    {
      public  Task<string> GetResponseAsync(Uri uri);
    }
}
