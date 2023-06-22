using GeoProf.DataBase;
using GeoProf.Entities;
using GeoProf.Enums;
using GeoProf.Models;
using GeoProf.Services;
using GeoProf.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeoProf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class VerlofController : BaseController
    {
        private readonly GeoProfContext dataContext;

        public VerlofController(GeoProfContext dataContext)
        {
            this.dataContext = dataContext;
        }
        //Post functie, alleen de werknemer en admin kunnen in deze functie.
        [HttpPost("VerlofAanvraag")]
        [JWTAuth(Role.werknemer | Role.admin)]
        public async Task<IActionResult> Post(VerlofCreateModel model)
        {
            //haal userid op.
            var result = TryGetUserId(out var userId);
            if (!result) return Unauthorized();

            //startdatum en eindatum setten en berekenen.
            var startDate = model.From;
            var endDate = model.Until;
            var totalDays = endDate - startDate;

            //return data
            var newVerlof = new Verlof
            {
                UserId = (int)userId,
                VerlofReden = model.VerlofReden,
                From = model.From,
                Until = model.Until,
                Beschrijving = model.Beschrijving,
                TotalDays = totalDays.Days + 1,
                IsPending = true,
                IsDenied = false,
                IsApproved = false,
            };

            //voeg data toe en save.
            await dataContext.Verlofs.AddAsync(newVerlof);
            await dataContext.SaveChangesAsync();

            return Ok();
        }

        //Get functie, zowel de werknemer, manager en admin kunnen in deze functie.
        [HttpGet()]
        [JWTAuth(Role.werknemer | Role.manager | Role.admin)]
        public async Task<ActionResult<VerlofTableModel>> Get()
        {
            //haal roles en userid op.
            var result = TryGetUserRoles(out var userRoles);
            if (!result) { return Unauthorized(); }

            var result2 = TryGetUserId(out var userId);
            if (!result2) { return Unauthorized(); }

            //hall alle verlofs op en zet naar een list.
            var query = dataContext.Verlofs
                .Include(x => x.User)
                .ThenInclude(u => u.Afdeling);

            var verlofs = await query.ToListAsync();

            //haal adeling op van een user.
            var user = await dataContext.Users.Include(a => a.Afdeling).FirstOrDefaultAsync(u => u.Id == userId);

            //return data.
            var models = verlofs.Select(Verlof => new VerlofTableModel
            {
                Id = Verlof.Id,
                UserId = Verlof.UserId,
                Username = Verlof.User.Username,
                VerlofReden = Verlof.VerlofReden,
                From = Verlof.From,
                Until = Verlof.Until,
                Beschrijving = Verlof.Beschrijving,
                IsPending = Verlof.IsPending,
                IsDenied = Verlof.IsDenied,
                IsApproved = Verlof.IsApproved,
                Vakantie = Verlof.User.Vakantie,
                Persoonlijk = Verlof.User.Persoonlijk,
                Ziek = Verlof.User.Ziek,
                afdelingsnaam = Verlof.User.Afdeling?.AfdelingNaam,
            });
            
            return Ok(models);
        }

        //Get functie, alleen de werknemer en admin kunnen in deze functie.
        [HttpGet("DaysTaken")]
        [JWTAuth(Role.werknemer | Role.admin)]
        public async Task<ActionResult<DaysTakenModel>> GetDaysTaken()
        {
            //haal userid op.
            var result = TryGetUserId(out var userId);
            if (!result) return Unauthorized();

            //haal userdata op.
            var user = await dataContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            //return data.
            return Ok(new DaysTakenModel
            {
                TotalDaysAvailable = user.Vakantie,
                DaysTaken = user.Vakantie - await dataContext.Verlofs.Where(x => x.UserId == userId && x.IsApproved && x.From.Year == DateTime.Now.Year).SumAsync(x => x.TotalDays)
            });
        }
    }
}
