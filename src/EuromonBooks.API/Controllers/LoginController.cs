using EuromonBooks.Abstractions.Models;
using EuromonBooks.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EuromonBooks.API.Controllers
{
    [ApiController]
    [Route(RouteHelper.BaseRoute)]
    public class LoginController : ControllerBase
    {
        private ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        /// <summary>
        /// Login in to the system
        /// </summary>
        /// <param name="userLogin">Login request</param>
        /// <returns></returns>
        [SwaggerOperation(Tags = new[] { "Authentication" })]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest userLogin)
        {
            var result = await _loginService.Login(userLogin);

            return Ok(new { UUid = result.UUid, Token = result.Token });
        }
    }
}