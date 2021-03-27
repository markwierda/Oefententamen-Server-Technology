using Microsoft.EntityFrameworkCore;
using Oefententamen.Models;

namespace Oefententamen.Data
{
    public class TheaterDbContext : DbContext
    {
        public TheaterDbContext(DbContextOptions<TheaterDbContext> options) : base(options)
        {
        }

        public DbSet<Klant> Klant { get; set; }
        public DbSet<Reservering> Reservering { get; set; }
    }
}