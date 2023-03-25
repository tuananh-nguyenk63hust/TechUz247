using Microsoft.EntityFrameworkCore;
namespace TechUZ247
{
    public class Database: DbContext
    {
        public DbSet<Users> Users { set; get; }
        public Database(DbContextOptions<Database> options) : base(options) { }
      
    }
}
