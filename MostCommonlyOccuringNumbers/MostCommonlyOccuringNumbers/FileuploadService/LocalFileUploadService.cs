using System.Text.RegularExpressions;

namespace MostCommonlyOccuringNumbers.FileuploadService
{
    public class LocalFileUploadService : IFileuploadService
    {
        private readonly IHostEnvironment _environment;
        public LocalFileUploadService(IHostEnvironment environment)
        {
            _environment = environment;
        }
        public async Task<int[]> UploadFileAsync(IFormFile file)
        {
            var filePath = Path.Combine(_environment.ContentRootPath, "files", file.FileName);
            using (var reader = File.OpenText(filePath))
            {
                var fileText = await reader.ReadToEndAsync();
                var newString = string.Join(" ", Regex.Split(fileText, @"(?:\r\n|\n|\r)")).Split(" ");
                Dictionary<int, int> numbersCount = new Dictionary<int, int>();
                foreach (var item in newString)
                {
                    if (item != "")
                    {
                        var num = Convert.ToInt16(item.ToString());

                        if (!numbersCount.TryGetValue(num, out int value))
                        {
                            numbersCount[num] = 1;
                        }
                        else
                        {
                            numbersCount[num]++;
                        }
                    }
                }

                return numbersCount.OrderByDescending(r => r.Value).Take(5).ToDictionary(x => x.Key).Keys.ToArray();
            }
        }
    }
}
