using EuromonBooks.Domain.Abstractions.SharedKeys;
using Microsoft.AspNetCore.Mvc;

namespace EuromonBooks.API.Controllers
{
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// The Auth Token used to authenticate the user
        /// </summary>
        [FromHeader(Name = HeaderKeys.Header_AuthToken)]
        public string AuthToken { get; set; }
    }
}