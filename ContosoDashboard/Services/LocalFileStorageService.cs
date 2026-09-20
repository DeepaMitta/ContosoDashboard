using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ContosoDashboard.Services
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _basePath;
        private readonly ILogger<LocalFileStorageService> _logger;

        public LocalFileStorageService(IConfiguration configuration, ILogger<LocalFileStorageService> logger)
        {
            _logger = logger;
            var relative = configuration.GetValue<string>("Uploads:Path") ?? "AppData/uploads";
            _basePath = Path.Combine(AppContext.BaseDirectory, "..", "..", relative);
            Directory.CreateDirectory(_basePath);
        }

        public async Task DeleteAsync(string relativePath)
        {
            var full = Path.Combine(_basePath, relativePath);
            if (File.Exists(full))
            {
                File.Delete(full);
            }
            await Task.CompletedTask;
        }

        public async Task<string> UploadAsync(Stream fileStream, string relativePath)
        {
            var full = Path.Combine(_basePath, relativePath);
            var dir = Path.GetDirectoryName(full);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir!);
            using var fs = new FileStream(full, FileMode.Create, FileAccess.Write);
            await fileStream.CopyToAsync(fs);
            return relativePath.Replace("\\", "/");
        }

        public async Task<Stream> DownloadAsync(string relativePath)
        {
            var full = Path.Combine(_basePath, relativePath);
            Stream ms = new FileStream(full, FileMode.Open, FileAccess.Read);
            return await Task.FromResult(ms);
        }

        public Task<string> GetUrlAsync(string relativePath)
        {
            // Local file storage does not provide public URLs; return local path for debugging
            return Task.FromResult(Path.Combine(_basePath, relativePath));
        }
    }
}
