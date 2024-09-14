using System.ComponentModel.DataAnnotations;
namespace Gallery.Models
{
    public class ImageInfo
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; } = String.Empty;
        public long FileSize { get; set; }
        public string FilePath { get; set; } = String.Empty;
    }
}
