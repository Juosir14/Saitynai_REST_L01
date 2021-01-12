using Microsoft.EntityFrameworkCore;
using Saitynai_REST_L01.Models;
 
namespace Saitynai_REST_L01.Data
{
    public class PlayerContext : DbContext
    {
        public PlayerContext(DbContextOptions<PlayerContext> opt) : base(opt)
        {

        }

        public DbSet<Player> Player { get; set; }

        
    }
}