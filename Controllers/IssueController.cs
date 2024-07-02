using GeoProf.Attributes;
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

    public class IssueController : BaseController
    {
        private readonly GeoProfContext dataContext;

        public IssueController(GeoProfContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpPost("create")]
        [JWTAuth(Role.admin)]
        public async Task<ActionResult> CreateIssue(IssueCreationModel model)
        {
            var selectedUser = await dataContext.Users.Where(u => u.Id != model.UserId).FirstOrDefaultAsync();

            if (selectedUser == null)
            {
                return BadRequest("User not found.");
            }

            var newIssue = new Issue
            {
                UserId = model.UserId,
                Name = model.IssueName,
                Description = model.IssueDescription,
                Hours = model.Hours,
                IsCompleted = false,
            };

            dataContext.Issues.Add(newIssue);
            await dataContext.SaveChangesAsync();

            return Ok();
        }
    }
}
