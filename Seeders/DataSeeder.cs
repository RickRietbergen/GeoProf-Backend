﻿using GeoProf.DataBase;
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

            var users = new List<User>
            {
                new User { Username = "rick", Password = "123", Vakantie = 20, Persoonlijk = 0, Ziek = 0, Afdeling = afdelingen[6], Role = Role.admin}, 
                new User { Username = "tijn", Password = "123", Vakantie = 20, Persoonlijk = 0, Ziek = 0, Afdeling = afdelingen[0], Role = Role.admin}, 
                new User { Username = "justin", Password = "123", Vakantie = 20, Persoonlijk = 0, Ziek = 0, Afdeling = afdelingen[4], Role = Role.manager}, 
                new User { Username = "stijn", Password = "123", Vakantie = 20, Persoonlijk = 0, Ziek = 0, Afdeling = afdelingen[3], Role = Role.werknemer},
                new User { Username = "covadis", Password = "123", Vakantie = 18, Persoonlijk = 0, Ziek = 0, Afdeling = afdelingen[5], Role = Role.werknemer},
            };

            var verlofs = new List<Verlof>
            {
                new Verlof { User = users[0], VerlofReden = "vacation", From = new DateTime(2023, 7, 12), Until = new DateTime(2023, 7, 15), Beschrijving = "ik wil graag verlof hebben", IsPending = true, IsDenied = false, IsApproved = false, TotalDays = 0},
                new Verlof { User = users[1], VerlofReden = "personal", From = new DateTime(2023, 7, 15), Until = new DateTime(2023, 7, 18), Beschrijving = "ik wil meer frontenden", IsPending = false, IsDenied = false, IsApproved = true, TotalDays = 0 },
                new Verlof { User = users[2], VerlofReden = "vacation", From = new DateTime(2023, 6, 18), Until = new DateTime(2023, 7, 21), Beschrijving = "ik kan iedereen verslaan", IsPending = true, IsDenied = false, IsApproved = false, TotalDays = 0 },
                new Verlof { User = users[3], VerlofReden = "sick", From = new DateTime(2023, 7, 9), Until = new DateTime(2023, 7, 11), Beschrijving = "ik wil niet blijven werken", IsPending = false, IsDenied = true, IsApproved = false, TotalDays = 0 },
                new Verlof { User = users[4], VerlofReden = "personal", From = new DateTime(2023, 7, 11), Until = new DateTime(2023, 7, 14), Beschrijving = "ik wil werken bij covadis", IsPending = true, IsDenied = false, IsApproved = false, TotalDays = 0 },
            };


            dataContext.afdelingen.AddRange(afdelingen);
            dataContext.Users.AddRange(users);
            dataContext.Verlofs.AddRange(verlofs);
            dataContext.SaveChanges();
        }
    }
}
