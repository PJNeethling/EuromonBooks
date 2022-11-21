namespace EuromonBooks.API
{
    public static class RouteHelper
    {
        public const string BaseRoute = "api/v{v:apiVersion}/[controller]";
        public const string BaseRouteNoController = "api/v{v:apiVersion}";
        public const string Users = "users";
        public const string User = "user/{id}";
        public const string RegisterUser = "user/register";
        public const string AssignUserRoles = "user/{id}/roles";
        public const string Roles = "roles";
        public const string Role = "role/{id}";
        public const string Books = "books";
        public const string AssignUserBooks = "user/{id}/books";
        public const string UserBooks = "user/{id}/books";
        public const string UserBook = "user/{id}/book/{bookId}";
    }
}