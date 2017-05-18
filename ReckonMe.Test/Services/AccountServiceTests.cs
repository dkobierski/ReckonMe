using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using ReckonMe.Helpers;
using ReckonMe.Models.Account;
using ReckonMe.Models.Security;
using ReckonMe.Services;

namespace ReckonMe.Test.Services
{
    [TestFixture]
    public class AccountServiceTests
    {
        public static readonly IDecodeToken TokenDecoder = Substitute.For<IDecodeToken>();
        public static readonly IRequstExecutor RequstExecutor = Substitute.For<IRequstExecutor>();

        public static void MockResponseStatusCodeForApiClient(HttpStatusCode statusCode, string token = null)
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                var tokenObject = new Token
                {
                    AccessToken = token,
                    ExpiresIn = 10
                };

                TokenDecoder.Decode(Arg.Any<HttpResponseMessage>())
                    .Returns(tokenObject);
            }

            RequstExecutor.PostAsync(Arg.Any<string>(), Arg.Any<StringContent>())
                .Returns(new HttpResponseMessage(statusCode));
        }

        public class Login
        {
            private readonly IAccountService _accountService;

            public Login()
            {
                _accountService = new AccountService(RequstExecutor, TokenDecoder);
            }

            [Test]
            public async Task Authenticated_when_api_response_is_ok()
            {
                MockResponseStatusCodeForApiClient(HttpStatusCode.OK);

                var result = await _accountService.LoginUserAsync(new AccountLoginData());

                result.Should().Be(AccountLoginResult.Authenticated);
            }

            [Test]
            public async Task RequestException_when_api_response_is_BadRequest()
            {
                MockResponseStatusCodeForApiClient(HttpStatusCode.BadRequest);

                var result = await _accountService.LoginUserAsync(new AccountLoginData());

                result.Should().Be(AccountLoginResult.RequestException);
            }


            [Test]
            public async Task InvalidCredential_when_api_response_is_Unauthorized()
            {
                MockResponseStatusCodeForApiClient(HttpStatusCode.Unauthorized);

                var result = await _accountService.LoginUserAsync(new AccountLoginData());

                result.Should().Be(AccountLoginResult.InvalidCredentials);
            }

            [Test]
            public async Task Sets_token_when_api_response_is_ok()
            {
                var expectedToken = "token";

                MockResponseStatusCodeForApiClient(HttpStatusCode.OK, expectedToken);

                var result = await _accountService.LoginUserAsync(new AccountLoginData());

                RequstExecutor.Received().SetAuthToken(expectedToken);
            }
        }

        public class Register
        {
            private readonly IAccountService _accountService;

            public Register()
            {
                _accountService = new AccountService(RequstExecutor, TokenDecoder);
            }

            [Test]
            public async Task AccountCreated_when_api_response_is_ok()
            {
                MockResponseStatusCodeForApiClient(HttpStatusCode.OK);

                var result = await _accountService.SignUpUserAsync(new AccountRegisterData());

                result.Should().Be(AccountRegisterResult.AccountCreated);
            }

            [Test]
            public async Task RequestException_when_api_response_is_BadRequest()
            {
                MockResponseStatusCodeForApiClient(HttpStatusCode.BadRequest);

                var result = await _accountService.SignUpUserAsync(new AccountRegisterData());

                result.Should().Be(AccountRegisterResult.RequestException);
            }


            [Test]
            public async Task AlreadyExist_when_api_response_is_Conflict()
            {
                MockResponseStatusCodeForApiClient(HttpStatusCode.Conflict);

                var result = await _accountService.SignUpUserAsync(new AccountRegisterData());

                result.Should().Be(AccountRegisterResult.AlreadyExist);
            }
        }
    }
}
