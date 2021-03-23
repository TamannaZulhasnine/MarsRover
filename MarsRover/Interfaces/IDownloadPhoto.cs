using System.Threading.Tasks;
namespace MarsRover.Interfaces
{
  public interface IDownloadPhoto
    {
       public Task DownloadPhotoAsync(string date, string key, string uri);
    }
}
