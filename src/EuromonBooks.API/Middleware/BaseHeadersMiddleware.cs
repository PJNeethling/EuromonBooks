using EuromonBooks.Abstractions.Exceptions;
using EuromonBooks.Domain.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace EuromonBooks.API.Middleware
{
    public class BaseHeadersMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtOptions _options;

        public BaseHeadersMiddleware(RequestDelegate next, IOptions<JwtOptions> options)
        {
            _next = next;
            _options = options.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            ValidateHeaders(context, _options);
            await _next.Invoke(context);
        }

        private static void ValidateHeaders(HttpContext context, JwtOptions opt)
        {
            AuthenticationHeaderValue authHeaderValue;
            if (context.Request.Headers.ContainsKey("Authorization") || context.Request.Headers.ContainsKey("AuthToken"))
            {
                var authorizationheader = context.Request.Headers["Authorization"];
                var authTokenheader = context.Request.Headers["AuthToken"];

                authHeaderValue = AuthenticationHeaderValue.Parse(authorizationheader.IsNullOrEmpty() ? authTokenheader : authorizationheader);
                if (string.IsNullOrWhiteSpace(authHeaderValue.ToString()))
                {
                    throw new ArgumentException($"Authorization token is required");
                }
            }
            else
            {
                throw new ArgumentException($"Authorization token is required");
            }

            SetAccessHeaders(context, authHeaderValue.Parameter.IsNullOrEmpty() ? authHeaderValue.Scheme : authHeaderValue.Parameter, opt);
        }

        private static void SetAccessHeaders(HttpContext context, string token, JwtOptions opt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = opt.JwtIssuer,
                    ValidAudience = opt.JwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(opt.JwtKey))
                }, out SecurityToken validatedToken);
            }
            catch
            {
                throw new ApiException(HttpStatusCode.BadRequest, "Authorization token is invalid");
            }

            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var roles = securityToken.Claims.First(x => x.Type == "Roles").Value;
            context.Request.Headers.Add("Roles", roles);
        }
    }
}