using EuromonBooks.Repository.Password_Manager.Infrastructure;

namespace EuromonBooks.Repository.Password_Manager
{
    internal class ScryptHash : IPasswordHash
    {
        private readonly int _iterations;
        private readonly int _blockSize;
        private readonly int _threads;

        public ScryptHash(PasswordHashOptions options)
        {
            _iterations = options.Iterations;
            _blockSize = options.BlockSize;
            _threads = options.Threads;
        }

        public string Encode(string password)
        {
            Scrypt.ScryptEncoder encoder = new Scrypt.ScryptEncoder(_iterations, _blockSize, _threads);
            return encoder.Encode(password);
        }

        public bool Compare(string password, string hashedPassword)
        {
            Scrypt.ScryptEncoder encoder = new Scrypt.ScryptEncoder(_iterations, _blockSize, _threads);
            return encoder.Compare(password, hashedPassword);
        }
    }
}