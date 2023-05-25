using GeoProf.DataBase;
using GeoProf.Entities;
using GeoProf.Enums;
using GeoProf.Models;
using GeoProf.Services;
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
        public async Task<ActionResult<Verlof>> Post(VerlofCreateModel model)
        {
            //var result = TryGetUserId(out var userId);

            //if (!result) return Unauthorized();

            var newVerlof = new Verlof
            {
                //UserId = (int)UserId,
                VerlofReden = model.VerlofReden,
                From = model.From,
                Until = model.Until,
                Beschrijving = model.Beschrijving,
                IsPending = model.IsPending = true,
                IsApproved = model.IsApproved = false,
            };


            await dataContext.Verlofs.AddAsync(newVerlof);
            await dataContext.SaveChangesAsync();

            return Ok();
        }
    }
}
