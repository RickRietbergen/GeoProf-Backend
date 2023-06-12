using GeoProf.DataBase;
using GeoProf.Entities;
using GeoProf.Enums;
using GeoProf.Migrations;
using GeoProf.Models;
using GeoProf.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeoProf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly GeoProfContext dataContext;
        private readonly Afdelingen afdelingen;
        private readonly JWTService jwtService;


        public AuthController(GeoProfContext dataContext, IConfiguration configuration)
        {
            this.dataContext = dataContext;
            jwtService = new JWTService(configuration.GetSection("AppSettings:Token").Value);
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserCreationModel model)
        {
            var user = await dataContext.Users.Where(g => g.Username == model.Username).FirstOrDefaultAsync();

            if (user != null)
            {
                return BadRequest("Username is already in use.");
            }

            var newUser = new User();

            newUser.Username = model.Username;
            newUser.Password = model.Password;
            newUser.Role = Role.werknemer;

            dataContext.Users.Add(newUser);
            dataContext.SaveChanges();

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginModel model)
        {
            var user = await dataContext.Users.Where(g => g.Username == model.Username).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound("User was not found");
            }

            if (model.Password != user.Password)
            {
                return BadRequest("Username or Password does not match");
            }

            var afdeling = await dataContext.afdelingen
                .FirstOrDefaultAsync(a => a.AfdelingId == user.AfdelingId);

            var afdelingNaam = afdeling?.AfdelingNaam;

            return Ok(new
            {
                token = jwtService.CreateJWT(user),
                user.Username,
                user.Persoonlijk,
                user.Vakantie,
                user.Ziek,
                user.Id,
                user.Role,
                afdelingnaam = afdelingNaam,
            });
        }
    }
}
