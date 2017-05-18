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
        public static readonly IRestApiClient ApiClient = Substitute.For<IRestApiClient>();
        public static readonly IDecodeToken TokenDecoder = Substitute.For<IDecodeToken>();
        public static readonly IRequstExecutor RequstExecutor = Substitute.For<IRequstExecutor>();

        public static void MockResposneStatusCodeForApiClient(HttpStatusCode statusCode, string token = null)
        {
            var message = new Func<HttpResponseMessage>(() => new HttpResponseMessage(statusCode));

            if (!string.IsNullOrWhiteSpace(token))
            {
                var task = new Task<Token>(() => new Token
                {
                    AccessToken = token,
                    ExpiresIn = 10
                });

                task.Start();
                task.Wait();

                TokenDecoder.Decode(Arg.Any<HttpResponseMessage>())
                    .Returns(task);
            }


            RequstExecutor.PostAsync(Arg.Any<string>(), Arg.Any<StringContent>())
                .Returns(new Task<HttpResponseMessage>(message));
        }

        public class Login
        {
            private readonly IAccountService _accountService;

            public Login()
            {
                _accountService = new AccountService(RequstExecutor, TokenDecoder);
            }

            [Test]
            public async Task Authenticated_when_api_resposne_is_ok()
            {
                MockResposneStatusCodeForApiClient(HttpStatusCode.OK);

                var result = await _accountService.LoginUserAsync(new AccountLoginData());

                result.Should().Be(AccountLoginResult.Authenticated);
            }

            [Test]
            public async Task RequestException_when_api_resposne_is_BadRequest()
            {
                MockResposneStatusCodeForApiClient(HttpStatusCode.BadRequest);

                var result = await _accountService.LoginUserAsync(new AccountLoginData());

                result.Should().Be(AccountLoginResult.RequestException);
            }


            [Test]
            public async Task InvalidCredential_when_api_resposne_is_Unauthorized()
            {
                MockResposneStatusCodeForApiClient(HttpStatusCode.Unauthorized);

                var result = await _accountService.LoginUserAsync(new AccountLoginData());

                result.Should().Be(AccountLoginResult.InvalidCredentials);
            }

            [Test]
            public async void Sets_token_when_api_resposne_is_ok()
            {
                var expectedToken = "token";

                MockResposneStatusCodeForApiClient(HttpStatusCode.OK, expectedToken);

                var result = await _accountService.LoginUserAsync(new AccountLoginData());

                ApiClient.Received().SetAuthToken(expectedToken);
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
            public async Task AccountCreated_when_api_resposne_is_ok()
            {
                MockResposneStatusCodeForApiClient(HttpStatusCode.OK);

                var result = await _accountService.SignUpUserAsync(new AccountRegisterData());

                result.Should().Be(AccountRegisterResult.AccountCreated);
            }

            [Test]
            public async Task RequestException_when_api_resposne_is_BadRequest()
            {
                MockResposneStatusCodeForApiClient(HttpStatusCode.BadRequest);

                var result = await _accountService.SignUpUserAsync(new AccountRegisterData());

                result.Should().Be(AccountRegisterResult.RequestException);
            }


            [Test]
            public async Task AlreadyExist_when_api_resposne_is_Conflict()
            {
                MockResposneStatusCodeForApiClient(HttpStatusCode.Conflict);

                var result = await _accountService.SignUpUserAsync(new AccountRegisterData());

                result.Should().Be(AccountRegisterResult.AlreadyExist);
            }
        }
    }
}
