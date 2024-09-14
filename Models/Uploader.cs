using System.ComponentModel.DataAnnotations;
namespace Gallery.Models
{
    public class Uploader
    {
        [Required(ErrorMessage = "Image field can't be empty")]
        public required IFormFile Image { get; set; }
    }
}
