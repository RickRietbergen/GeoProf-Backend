using GeoProf.Attributes;
using GeoProf.DataBase;
using GeoProf.Entities;
using GeoProf.Enums;
using GeoProf.Models;
using GeoProf.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace GeoProf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController
    {
        private readonly GeoProfContext dataContext;
        private readonly IConfiguration configuration;
        private readonly JWTService jwtService;


        public AuthController(GeoProfContext dataContext, IConfiguration configuration)
        {
            this.dataContext = dataContext;
            this.configuration = configuration;
            jwtService = new JWTService(configuration.GetSection("AppSettings:Token").Value);
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserCreationModel model)
        {
            var user = await dataContext.Users.Where(g => g.Email == model.Email).FirstOrDefaultAsync();

            if (user != null)
            {
                return BadRequest("Email is already in use.");
            }

            var newUser = new User();
            CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);

            newUser.Name = model.Name;
            newUser.Email = model.Email;
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;
            newUser.Role = Role.Customer;

            dataContext.Users.Add(newUser);
            dataContext.SaveChanges();

            return Ok(newUser);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginModel model)
        {
            var user = await dataContext.Users.Where(g => g.Email == model.Email).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound("User was not found");
            }

            if (!VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Email or Password does not match");
            }

            return Ok(jwtService.CreateJWT(user));
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
