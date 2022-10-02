using Microsoft.EntityFrameworkCore;

namespace ImageApi_Task1.Model
{
    public class ImageDbContext : DbContext
    {
        public ImageDbContext(DbContextOptions option) : base(option)
        {

        }

        public DbSet<ImageModel> Iamges { get; set; }
    }
}
