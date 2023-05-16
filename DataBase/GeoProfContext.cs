using Microsoft.EntityFrameworkCore;

namespace GeoProf.DataBase
{
    public class GeoProfContext : DbContext
    {
        public GeoProfContext(DbContextOptions<GeoProfContext> context) : base(context)
        {

        }
    }
}
