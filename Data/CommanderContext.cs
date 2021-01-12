using Microsoft.EntityFrameworkCore;
using Saitynai_REST_L01.Models;
 
namespace Saitynai_REST_L01.Data
{
    public class CommanderContext : DbContext
    {
        public CommanderContext(DbContextOptions<CommanderContext> opt) : base(opt)
        {

        }

        public DbSet<Command> Commands { get; set; }

    }
}