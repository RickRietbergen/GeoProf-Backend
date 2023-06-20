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

        [HttpPost("VerlofAanvraag")]
        [JWTAuth(Role.werknemer | Role.admin)]
        public async Task<IActionResult> Post(VerlofCreateModel model)
        {
            var result = TryGetUserId(out var userId);
            if (!result) return Unauthorized();

            var startDate = model.From;
            var endDate = model.Until;
            var totalDays = endDate - startDate;

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

            await dataContext.Verlofs.AddAsync(newVerlof);
            await dataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet()]
        [JWTAuth(Role.werknemer | Role.manager | Role.admin)]
        public async Task<ActionResult<VerlofTableModel>> Get()
        {
            var result = TryGetUserRoles(out var userRoles);
            if (!result) { return Unauthorized(); }

            var result2 = TryGetUserId(out var userId);
            if (!result2) { return Unauthorized(); }

            var query = dataContext.Verlofs
                .Include(x => x.User)
                .ThenInclude(u => u.Afdeling);

            var verlofs = await query.ToListAsync();

            var user = await dataContext.Users.Include(a => a.Afdeling).FirstOrDefaultAsync(u => u.Id == userId);

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

        [HttpGet("DaysTaken")]
        [JWTAuth(Role.werknemer | Role.admin)]
        public async Task<ActionResult<DaysTakenModel>> GetDaysTaken()
        {
            var result = TryGetUserId(out var userId);
            if (!result) return Unauthorized();

            var user = await dataContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            return Ok(new DaysTakenModel
            {
                TotalDaysAvailable = user.Vakantie,
                DaysTaken = user.Vakantie - await dataContext.Verlofs.Where(x => x.UserId == userId && x.IsApproved && x.From.Year == DateTime.Now.Year).SumAsync(x => x.TotalDays)
            });
        }
    }
}
