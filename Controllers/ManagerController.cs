using GeoProf.DataBase;
using GeoProf.Enums;
using GeoProf.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeoProf.Models;

namespace GeoProf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : BaseController
    {
        private readonly GeoProfContext dataContext;

        public ManagerController(GeoProfContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet("dashboard")]
        [JWTAuth(Role.manager | Role.admin)]
        public async Task<ActionResult<ManagerDashboard>> Dashboard()
        {
            var users = await dataContext.Users.ToListAsync();
            var verlofs = await dataContext.Verlofs.Where(v => v.IsPending == true).ToListAsync();
            var employeeIsSick = await dataContext.Verlofs.Where(e => e.VerlofReden == "Sick").ToListAsync();

            var amountOfUsers = users.Count;
            var amountOfVerlofs = verlofs.Count;
            var amountEmployeeIsSick = employeeIsSick.Count;

            return Ok(new ManagerDashboard
            {
                users = amountOfUsers,
                verlofs = amountOfVerlofs,
                employeeIsSick = amountEmployeeIsSick,
            });
        }
    }
}
