using GeoProf.Entities;
using GeoProf.Migrations;
using Microsoft.EntityFrameworkCore;

namespace GeoProf.DataBase
{
    public class GeoProfContext : DbContext
    {
        public GeoProfContext(DbContextOptions<GeoProfContext> context) : base(context)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Verlof> Verlofs { get; set; }
        public DbSet<Afdeling> afdelingen { get; set; }
    }
}
