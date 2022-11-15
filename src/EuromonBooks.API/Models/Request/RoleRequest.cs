namespace EuromonBooks.API.Models.Request
{
    public class RoleRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}