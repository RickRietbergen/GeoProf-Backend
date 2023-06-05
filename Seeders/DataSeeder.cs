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

            var verlofs = new List<Verlof>
            {
                new Verlof { UserId = 1, VerlofReden = "vacation", From = new DateTime(2023, 6, 6), Until = new DateTime(2023, 6, 8), Beschrijving = "ik wil graag verlof hebben", IsPending = true, IsDenied = false, IsApproved = false },
                new Verlof { UserId = 2, VerlofReden = "personal", From = new DateTime(2023, 6, 7), Until = new DateTime(2023, 6, 9), Beschrijving = "ik wil meer frontenden", IsPending = true, IsDenied = false, IsApproved = false },
                new Verlof { UserId = 3, VerlofReden = "vacation", From = new DateTime(2023, 6, 11), Until = new DateTime(2023, 6, 14), Beschrijving = "ik kan iedereen verslaan", IsPending = true, IsDenied = false, IsApproved = false },
                new Verlof { UserId = 4, VerlofReden = "sick", From = new DateTime(2023, 6, 13), Until = new DateTime(2023, 6, 17), Beschrijving = "ik wil niet blijven gpt'en", IsPending = true, IsDenied = false, IsApproved = false },
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
            dataContext.Verlofs.AddRange(verlofs);
            dataContext.afdelingen.AddRange(afdelingen);
            dataContext.SaveChanges();
        }
    }
}
