using BomberMan.Shared;
using Microsoft.EntityFrameworkCore;

namespace BomberMan.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<PlayerScore> PlayerScores { get; set; }
    }
}