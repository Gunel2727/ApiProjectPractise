using Microsoft.EntityFrameworkCore;

namespace ApiProjectPractise.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
       
    }
}
