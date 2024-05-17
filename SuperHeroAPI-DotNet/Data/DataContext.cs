using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SuperHeroAPI_DotNet.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SuperHeroAPI_DotNet.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<SuperHero> SuperHeroes { get; set; }
    }
}
