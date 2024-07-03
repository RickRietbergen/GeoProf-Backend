using GeoProf.Attributes;
using GeoProf.DataBase;
using GeoProf.Entities;
using GeoProf.Enums;
using GeoProf.Models.Admin.Issue;
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

        [HttpPut("edit")]
        [JWTAuth(Role.admin)]
        public async Task<ActionResult> EditIssue(IssueEditModel model)
        {
            var selectedIssue = await dataContext.Issues.Where(i => i.Id == model.Id).FirstOrDefaultAsync();

            if (selectedIssue == null)
            {
                return BadRequest("Issue not found.");
            }

            var selectedUser = await dataContext.Users.Where(u => u.Id != model.UserId).FirstOrDefaultAsync();

            if (selectedUser == null)
            {
                return BadRequest("User not found.");
            }

            selectedIssue.UserId = model.UserId;
            selectedIssue.Name = model.IssueName;
            selectedIssue.Description = model.IssueDescription;
            selectedIssue.Hours = model.Hours;
            selectedIssue.IsCompleted = model.IsCompleted;

            await dataContext.SaveChangesAsync();

            return Ok();
        }
    }
}
