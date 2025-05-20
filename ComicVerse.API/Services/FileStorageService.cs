namespace ComicVerse.API.Services
{
    public interface IFileStorageService
    {
        Task<string> SaveFile(IFormFile file, string subDirectory);
        Task DeleteFile(string filePath);
        string GetFileUrl(string filePath);
    }

    public class LocalFileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LocalFileStorageService(
            IWebHostEnvironment env,
            IHttpContextAccessor httpContextAccessor)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> SaveFile(IFormFile file, string subDirectory)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Arquivo inválido");
            }

            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", subDirectory);
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.Combine("uploads", subDirectory, uniqueFileName);
        }

        public async Task DeleteFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            var fullPath = Path.Combine(_env.WebRootPath, filePath);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        public string GetFileUrl(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return string.Empty;
            }

            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null)
            {
                return string.Empty;
            }

            return $"{request.Scheme}://{request.Host}/{filePath.Replace("\\", "/")}";
        }
    }
}