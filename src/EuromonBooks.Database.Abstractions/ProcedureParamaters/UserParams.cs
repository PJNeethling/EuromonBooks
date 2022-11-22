namespace EuromonBooks.Database.Abstractions.ProcedureParamaters
{
    public class UserParams
    {
        public Guid? Uuid { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PassPhrase { get; set; }
        public int? StatusId { get; set; }
    }
}