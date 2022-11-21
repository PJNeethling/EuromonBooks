using AutoFixture.Xunit2;
using EuromonBooks.Abstractions.Models;
using EuromonBooks.Abstractions.Repositories;
using EuromonBooks.Abstractions.Services.JwtService;
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
            [Frozen] IJwtService jwtService,
            LoginService sut,
            UserLoginRequest request,
            AccessTokenLoginResponse response,
            string token)
        {
            repo.UserLogin(Arg.Any<UserLoginRequest>()).Returns(response);
            jwtService.GenerateJwtToken(Arg.Any<UserAccessInfo>()).Returns(token);

            var result = await sut.Login(request);

            Assert.Equal(response.UUid, result.UUid);
            Assert.Equal(token, result.Token);
        }
    }
}