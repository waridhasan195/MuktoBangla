using Microsoft.EntityFrameworkCore;
using MuktoBangla.Model.Domain;

namespace MuktoBangla.Data
{
    public class MuktoBanglaDbContext : DbContext
    {
        public MuktoBanglaDbContext(DbContextOptions<MuktoBanglaDbContext> options) 
            : base(options) { }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
