using GeoProf.DataBase;
using GeoProf.Entities;

namespace GeoProf.Seeders
{
    public class DataSeeder
    {
        private readonly GeoProfContext dataContext;

        public DataSeeder(GeoProfContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void Seed()
        {
            var afdelingen = new List<Afdeling>
            {
                new Afdeling { AfdelingNaam = "Office Management" },
                new Afdeling { AfdelingNaam = "Geo ICT" },
                new Afdeling { AfdelingNaam = "geodesy" },
                new Afdeling { AfdelingNaam = "relation managment" },
                new Afdeling { AfdelingNaam = "finance" },
                new Afdeling { AfdelingNaam = "HRM" },
                new Afdeling { AfdelingNaam = "ICT" }
            };

            dataContext.afdelingen.AddRange(afdelingen);
            dataContext.SaveChanges();
        }
    }
}
