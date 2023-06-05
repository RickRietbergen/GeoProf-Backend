using GeoProf.DataBase;
using GeoProf.Entities;
using GeoProf.Enums;

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
            var users = new List<User>
            {
                new User { Username = "rick", Password = "123", Vakantie = 20, Persoonlijk = 0, Ziek = 0, AfdelingId = 1, Role = Role.admin}, 
                new User { Username = "tijn", Password = "123", Vakantie = 20, Persoonlijk = 0, Ziek = 0, AfdelingId = 1, Role = Role.admin}, 
                new User { Username = "justin", Password = "123", Vakantie = 20, Persoonlijk = 0, Ziek = 0, AfdelingId = 1, Role = Role.admin}, 
                new User { Username = "stijn", Password = "123", Vakantie = 20, Persoonlijk = 0, Ziek = 0, AfdelingId = 1, Role = Role.admin}, 
            };

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

            dataContext.Users.AddRange(users);
            dataContext.afdelingen.AddRange(afdelingen);
            dataContext.SaveChanges();
        }
    }
}
