using Microsoft.EntityFrameworkCore;
using SortNumbers.Models;

namespace SortNumbers.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }

        public DbSet<SortResult> SortResults { get; set; }

        public DbSet<SortedNumber> SortedNumbers { get; set; }
    }
}
