using MarsRover.Models;
using System.Threading.Tasks;

namespace MarsRover.Interfaces
{
    public interface IRoverApiManager
    {
       string Uri { get; set; }
       Rover Rover { get; set; }
       void DownloadPhotoForRoverAsync(string date, string key, Rover rover);
    }
}