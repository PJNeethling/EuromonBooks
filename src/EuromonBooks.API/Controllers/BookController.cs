using EuromonBooks.Abstractions.Attributes;
using EuromonBooks.Abstractions.Models;
using EuromonBooks.Domain.Abstractions.Models;
using EuromonBooks.Domain.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EuromonBooks.API.Controllers
{
    [ApiController]
    [Route(RouteHelper.BaseRouteNoController)]
    public class BookController : BaseController
    {
        private IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// View all books in the system
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation(Tags = new[] { "Book" })]
        [AllowedAccessAttribute("2")]
        [Route(RouteHelper.Books)]
        [HttpGet]
        public async Task<AllBooks> GetAllBooks()
        {
            var result = await _bookService.GetAllBooks();

            return result;
        }

        /// <summary>
        /// View all books for a user
        /// </summary>
        /// <param name="id">User identification id</param>
        /// <returns></returns>
        [SwaggerOperation(Tags = new[] { "Book" })]
        [AllowedAccessAttribute("2")]
        [Route(RouteHelper.UserBooks)]
        [HttpGet]
        public async Task<AllBooks> GetAllBooksForUser(string id)
        {
            var result = await _bookService.GetAllBooksForUser(id);

            return result;
        }

        /// <summary>
        /// Assign books to user.
        /// </summary>
        /// <param name="id">User identification id</param>
        /// <param name="bookIds">Book Id's to assign</param>
        /// <returns>Ok</returns>
        [SwaggerOperation(Tags = new[] { "UserBookAssignment" })]
        [AllowedAccessAttribute("2")]
        [HttpPut]
        [Route(RouteHelper.AssignUserBooks)]
        public async Task<ActionResult> AssignBooksToUser(string id, IdList bookIds)
        {
            await _bookService.AssignBooksToUser(id, bookIds);

            return Ok();
        }
    }
}