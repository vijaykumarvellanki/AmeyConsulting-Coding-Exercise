namespace MostCommonlyOccuringNumbers.FileuploadService
{
    public interface IFileuploadService
    {
        Task<int[]> UploadFileAsync(IFormFile file);
    }
}
