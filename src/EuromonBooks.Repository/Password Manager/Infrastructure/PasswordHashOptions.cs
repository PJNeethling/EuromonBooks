namespace EuromonBooks.Repository.Password_Manager.Infrastructure
{
    public class PasswordHashOptions
    {
        public int Iterations { get; set; }
        public int BlockSize { get; set; }
        public int Threads { get; set; }

        public static PasswordHashOptions Standard
        {
            get
            {
                return new PasswordHashOptions()
                {
                    Iterations = 16,
                    BlockSize = 16,
                    Threads = 2
                };
            }
        }
    }
}