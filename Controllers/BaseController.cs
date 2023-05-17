using GeoProf.Enums;
using Microsoft.AspNetCore.Mvc;

namespace GeoProf.Controllers
{
    public class BaseController : ControllerBase
    {
        protected bool TryGetUserId(out int? userId)
        {
            var result = HttpContext.Items.TryGetValue("user", out var user);

            if (result)
            {
                userId = (int)user;
                return true;
            }
            else
            {
                userId = null;
                return false;
            }
        }

        protected bool TryGetUserRoles(out Role? userRoles)
        {
            var result = HttpContext.Items.TryGetValue("roles", out var roles);

            if (result)
            {
                userRoles = (Role)roles;
                return true;
            }
            else
            {
                userRoles = null;
                return false;
            }
        }
    }
}
