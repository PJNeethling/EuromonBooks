namespace EuromonBooks.Repository.Password_Manager.Infrastructure
{
    public interface IPassword
    {
        string HashPassword(string password);

        bool ComparePassword(string password, string hashedPassword);
    }
}