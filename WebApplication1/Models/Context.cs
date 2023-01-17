using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Information> Information { get; set; }
    }
}
