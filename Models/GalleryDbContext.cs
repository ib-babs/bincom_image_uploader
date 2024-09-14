using Microsoft.EntityFrameworkCore;
namespace Gallery.Models
{
    public class GalleryDbContext(DbContextOptions<GalleryDbContext> options) : DbContext(options)
    {
        public DbSet<ImageInfo> Images => Set<ImageInfo>();
    }
}
