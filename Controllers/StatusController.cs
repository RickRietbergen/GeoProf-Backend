using GeoProf.DataBase;
using GeoProf.Enums;
using GeoProf.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeoProf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class StatusController : BaseController
    {
        private readonly GeoProfContext dataContext;

        public StatusController(GeoProfContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpPut("denied/{id}")]
        [JWTAuth(Role.manager | Role.admin)]
        public async Task<IActionResult> Denied(int id)
        {
            var selectedAanvraag = await dataContext.Verlofs.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (selectedAanvraag == null)
            {
                return BadRequest("Verlof Request does not exist");
            }
            selectedAanvraag.IsPending = false;
            selectedAanvraag.IsDenied = true;

            await dataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("approved/{id}")]
        [JWTAuth(Role.manager | Role.admin)]
        public async Task<IActionResult> Approved(int id)
        {
            var selectedAanvraag = await dataContext.Verlofs.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (selectedAanvraag == null)
            {
                return BadRequest("Verlof Request does not exist");
            }
            selectedAanvraag.IsPending = false;
            selectedAanvraag.IsApproved = true;

            await dataContext.SaveChangesAsync();

            return Ok();
        }
    }
}
