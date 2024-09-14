using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gallery.Models;
using Microsoft.EntityFrameworkCore;

namespace Gallery.Controllers;

public class HomeController(ILogger<HomeController> logger, GalleryDbContext context) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;


    public IActionResult Index()
    {
        IEnumerable<ImageInfo> imagesInfo = context.Images;

        return View(imagesInfo);
    }

    [HttpPost]
    public async Task<IActionResult> UploadImage(Uploader uploader)
    {
        if (ModelState.IsValid)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (uploader.Image != null && uploader.Image.Length > 0)
            {

                DirectoryInfo imgPath = new(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploaded-images"));
                if (!imgPath.Exists)
                    imgPath.Create();
                var uniqueFileName = Guid.NewGuid().ToString() + uploader.Image.FileName;
                var filePath = Path.Combine(imgPath.FullName, uniqueFileName);
                using (var fStream = new FileStream(filePath, FileMode.Create))
                {
                    await uploader.Image.CopyToAsync(fStream);
                }
                ImageInfo imageUpload = new() { FileName = uniqueFileName, FilePath= filePath, FileSize = uploader.Image.Length };
                context.Images.Add(imageUpload);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }
        return View();
    }
    [HttpGet]
    public IActionResult UploadImage()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ImagePreview(int id)
    {
        try
        {
            ImageInfo img = context.Images.Where(image => image.Id == id).First();
            return View(img);
        }
        catch{ }
        return RedirectToAction("Index");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
