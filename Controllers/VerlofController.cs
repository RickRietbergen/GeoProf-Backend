using GeoProf.DataBase;
using GeoProf.Entities;
using GeoProf.Enums;
using GeoProf.Models;
using GeoProf.Services;
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
        public async Task<IActionResult> Post(Model name)
        {
            var result = TryGetUserId(out var userId);

            if (!result) return Unauthorized();
        }
    }
}
