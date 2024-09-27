using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UploadFiles.Pages;

public class IndexModel : PageModel
{
    private IWebHostEnvironment Environment;
    public string Message { get; set; }

    public IndexModel(IWebHostEnvironment _environment)
    {
        this.Environment = _environment;
    }

    public void OnGet()
    {

    }

    public void OnPostUpload(List<IFormFile> postedFiles)
    {
        string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        foreach (IFormFile postedFile in postedFiles)
        {
            string fileName = Path.GetFileName(postedFile.FileName);
            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                postedFile.CopyTo(stream);
                this.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
            }
        }
    }
}
