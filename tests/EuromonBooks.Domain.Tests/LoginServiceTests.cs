using AutoFixture.Xunit2;
using EuromonBooks.Abstractions.Models;
using EuromonBooks.Abstractions.Repositories;
using EuromonBooks.TestHelpers;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace EuromonBooks.Domain.Tests
{
    public class LoginServiceTests
    {
        [TheoryAutoDisplayName, AutoNSubstituteData]
        public async Task Login_Returns_Success(
            [Frozen] ILoginRepository repo,
            LoginService sut,
            UserLoginRequest request,
            AccessTokenLoginResponse response)
        {
            repo.UserLogin(Arg.Any<UserLoginRequest>()).Returns(response);

            var result = await sut.Login(request);

            Assert.Equal(response.UUid, result.UUid);
            Assert.False(string.IsNullOrEmpty(result.Token));
        }
    }
}