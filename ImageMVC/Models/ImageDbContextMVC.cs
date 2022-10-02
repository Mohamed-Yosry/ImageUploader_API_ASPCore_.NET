using Microsoft.EntityFrameworkCore;

namespace ImageMVC.Models
{
    public class ImageDbContextMVC : DbContext
    {
        public ImageDbContextMVC(DbContextOptions opt) : base(opt)
        {
            
        }

        public DbSet<ImageModel> Images { get; set; }
    }
}
