using GeoProf.DataBase;
using GeoProf.Entities;
using GeoProf.Enums;
using GeoProf.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeoProf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly GeoProfContext dataContext;


        public AuthController(GeoProfContext dataContext)
        {
            this.dataContext = dataContext;
        }

        //[HttpPost("register")]
        //public async Task<ActionResult<User>> Register(UserCreationModel model)
        //{
        //    var user = await dataContext.Users.Where(g => g.Username == model.Username).FirstOrDefaultAsync();

        //    if (user != null)
        //    {
        //        return BadRequest("Username is already in use.");
        //    }

        //    var newUser = new User();

        //    newUser.Username = model.Username;
        //    newUser.Password = model.Password;
        //    newUser.Role = Role.werknemer;

        //    dataContext.Users.Add(newUser);
        //    dataContext.SaveChanges();

        //    return Ok();
        //}

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginModel model)
        {
            var user = await dataContext.Users.Where(g => g.Username == model.Username).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound("User was not found");
            }

            if (user.Password == null)
            {
                return BadRequest("Password does not match");
            }

            return Ok(new
            {
                user = user.Id,
                userName = user.Username,
                password = user.Password,
            });
        }
    }
}
