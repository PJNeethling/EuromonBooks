using EuromonBooks.Repository.Password_Manager.Infrastructure;

namespace EuromonBooks.Repository.Password_Manager
{
    public class Password : IPassword
    {
        private readonly PasswordHashComplexity _complexity = PasswordHashComplexity.StandardScrypt;

        public string HashPassword(string password)
        {
            IPasswordHash hasher = PasswordHashFactory.GetInstance(_complexity);
            string hashedPassword = hasher.Encode(password);
            hashedPassword = string.Format("{0}{1}", GetComplexityCharacter(_complexity), hashedPassword);
            return hashedPassword;
        }

        public bool ComparePassword(string password, string hashedPassword)
        {
            char complexityCharacter = hashedPassword[0];
            IPasswordHash hasher = PasswordHashFactory.GetInstance(GetComplexityFromCharacter(complexityCharacter));
            return hasher.Compare(password, hashedPassword.Remove(0, 1));
        }

        private static char GetComplexityCharacter(PasswordHashComplexity complexity)
        {
            byte value = (byte)((int)complexity + 64);
            return (char)value;
        }

        private static PasswordHashComplexity GetComplexityFromCharacter(char character)
        {
            int value = ((byte)character - 64);
            return (PasswordHashComplexity)value;
        }
    }
}