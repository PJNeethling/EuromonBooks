using EuromonBooks.Abstractions.Exceptions;
using EuromonBooks.Abstractions.Models;
using EuromonBooks.Abstractions.Repositories;
using EuromonBooks.Database.Abstractions;
using EuromonBooks.Domain.Abstractions.Models;
using EuromonBooks.Repository.Password_Manager.Infrastructure;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;

namespace EuromonBooks.Repository
{
    public class LoginRepository : BaseRepository, ILoginRepository
    {
        private readonly IDatabase _database;
        private readonly IPassword _password;
        private readonly EncryptionOptions _options;

        public LoginRepository(IDatabase database, IPassword password, IOptions<EncryptionOptions> options)
        {
            _database = database;
            _password = password;
            _options = options.Value;
        }

        public async Task<LoginResponse> UserLogin(UserLoginRequest request)
        {
            try
            {
                var userDetails = await _database.Login(request.Username, _options.Passphrase);
                var result = new LoginResponse() { Roles = new List<IdResponse>() };

                if (userDetails != null && _password.ComparePassword(request.Password, userDetails.Password))
                {
                    result.UUid = userDetails.Uuid;
                    if (userDetails.Roles != null)
                    {
                        result.Roles = JsonConvert.DeserializeObject<List<IdResponse>>(userDetails.Roles);
                    }
                }
                else
                {
                    throw new ApiException(HttpStatusCode.Unauthorized, "Incorrect credentials. Please try again.");
                }

                return new LoginResponse
                {
                    UUid = userDetails.Uuid,
                    Roles = result.Roles
                };
            }
            catch (SqlException ex)
            {
                throw HandleSqlException(ex);
            }
        }
    }
}