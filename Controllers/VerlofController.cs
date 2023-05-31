using GeoProf.DataBase;
using GeoProf.Entities;
using GeoProf.Enums;
using GeoProf.Models;
using GeoProf.Services;
using GeoProf.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

            var newVerlof = new Verlof
            {
                UserId = (int)userId,
                VerlofReden = model.VerlofReden,
                From = model.From,
                Until = model.Until,
                Beschrijving = model.Beschrijving,
                IsPending = true,
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
            if (!result) { return Unauthorized(); }

            var query = dataContext.Verlofs
                .Include(x => x.User);

            var verlofs = await query.ToListAsync();

            //if (userRoles != Role.manager)
            //{

            //}
            //else
            //{

            //}

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
                IsApproved = Verlof.IsApproved,
                Vakantie = Verlof.User.Vakantie,
                Persoonlijk = Verlof.User.Persoonlijk,
                Ziek = Verlof.User.Ziek,
            });
            
            return Ok(models);
        }
    }
}
