using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MostCommonlyOccuringNumbers.FileuploadService;

namespace MostCommonlyOccuringNumbers.Pages
{
    public class IndexModel : PageModel
    {
        [ViewData]
        public string Title { get; set; }

        private readonly ILogger<IndexModel> _logger;
        private readonly IFileuploadService _fileuploadService;

        public string[] filePath;

        public IndexModel(ILogger<IndexModel> logger, IFileuploadService fileuploadService)
        {
            _logger = logger;
            _fileuploadService = fileuploadService;
        }

        public void OnGet()
        {

        }

        public ActionResult OnPost(IFormFile file)
        {
            if (file != null)
            {
              var data =   _fileuploadService.UploadFileAsync(file).Result;
              ViewData["data"] = data;
            }
            return null;
        }
    }
}