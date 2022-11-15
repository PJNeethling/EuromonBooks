using EuromonBooks.Abstractions.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace EuromonBooks.Abstractions.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class AllowedAccessAttribute : ActionFilterAttribute
    {
        public string Roles { get; private set; }

        public AllowedAccessAttribute(string roles)
        {
            Roles = roles;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (string.IsNullOrWhiteSpace(context.HttpContext.Request.Headers["Roles"].ToString()))
            {
                throw new ApiException(HttpStatusCode.Forbidden, "You do not have access or permission at this time.");
            }

            var allowedRoles = Array.ConvertAll(Roles.Split(','), int.Parse);

            var userRoles = Array.ConvertAll(context.HttpContext.Request.Headers["Roles"].ToString().Split(","), int.Parse);

            foreach (var item in allowedRoles)
            {
                if (!userRoles.Contains(item))
                {
                    throw new ApiException(HttpStatusCode.Forbidden, "You do not have access or permission at this time.");
                }
            }
        }
    }
}