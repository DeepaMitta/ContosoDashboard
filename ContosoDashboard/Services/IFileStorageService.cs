using System.IO;
using System.Threading.Tasks;

namespace ContosoDashboard.Services
{
    public interface IFileStorageService
    {
        Task<string> UploadAsync(Stream fileStream, string relativePath);
        Task DeleteAsync(string relativePath);
        Task<Stream> DownloadAsync(string relativePath);
        Task<string> GetUrlAsync(string relativePath);
    }
}
