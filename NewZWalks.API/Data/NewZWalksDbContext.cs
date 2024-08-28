using Microsoft.EntityFrameworkCore;
using NewZWalks.API.Models.Domain;

namespace NewZWalks.API.Data
{
    public class NewZWalksDbContext: DbContext
    {
        public NewZWalksDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
