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

        //Get functie, alleen de manager en admin kunnen in deze functie.
        [HttpGet("dashboard")]
        [JWTAuth(Role.manager | Role.admin)]
        public async Task<ActionResult<ManagerDashboard>> Dashboard()
        {
            //haal alle data op die we displayen op het manager dashboard.
            var users = await dataContext.Users.Where(u => u.Role == Role.werknemer).ToListAsync();
            var verlofs = await dataContext.Verlofs.Where(v => v.IsPending == true).ToListAsync();
            var employeeIsSick = await dataContext.Verlofs.Where(e => e.VerlofReden == "Sick").ToListAsync();

            //set var
            var amountOfUsers = users.Count;
            var amountOfVerlofs = verlofs.Count;
            var amountEmployeeIsSick = employeeIsSick.Count;

            //return
            return Ok(new ManagerDashboard
            {
                users = amountOfUsers,
                verlofs = amountOfVerlofs,
                employeeIsSick = amountEmployeeIsSick,
            });
        }
    }
}
