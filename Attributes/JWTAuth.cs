using GeoProf.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using GeoProf.Services;
using System.Security.Claims;

namespace GeoProf.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JWTAuth : Attribute, IAuthorizationFilter
    {
        private readonly JWTService jwtService;
        private readonly string secret = "8937239492972280";
        private readonly Role roles;

        public JWTAuth(Role roles)
        {
            jwtService = new JWTService(secret);
            this.roles = roles;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            var token = context.HttpContext.Request.Headers["Bearer"];

            if (token.Count < 0)
            {
                Unauthorized(context);
            }
            else
            {
                if (jwtService.ValidateAndReadJWT(token.FirstOrDefault(), out var decodedToken))
                {
                    var userRole = Enum.Parse<Role>(decodedToken.Claims.First(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value);
                    if (!roles.HasFlag(userRole))
                    {
                        Unauthorized(context);
                    }
                    else
                    {
                        context.HttpContext.Items.Add("user", int.Parse(decodedToken.Claims.First(x => x.Type == "user").Value));
                        context.HttpContext.Items.Add("roles", userRole);
                    }
                }
                else
                {
                    Unauthorized(context);
                }
            }
        }

        private void Unauthorized(AuthorizationFilterContext context)
        {
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}
