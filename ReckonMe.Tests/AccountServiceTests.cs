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

namespace ReckonMe.Tests
{
    [TestFixture]
    public class AccountServiceTests
    {
        protected static readonly IRestApiClient ApiClient = Substitute.For<IRestApiClient>();
        protected static readonly IDecodeToken TokenDecoder = Substitute.For<IDecodeToken>();
        protected static readonly IRequstExecutor RequstExecutor = Substitute.For<IRequstExecutor>();

        protected static void MockResposneStatusCodeForApiClient(HttpStatusCode statusCode, string token = null)
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

        [Test]
        public void Authenticated_when_api_resposne_is_ok()
        {
            return;
        }

        [TestFixture]
        public class Login
        {
            private readonly IAccountService _accountService;

            public Login()
            {
                _accountService = new AccountService(RequstExecutor, TokenDecoder);
            }


            [Test]
            public void Authenticated_when_api_resposne_is_ok()
            {
                var task = Task.Run(_authenticated_when_api_resposne_is_ok);
                task.Wait();
            }

            [Test]
            public async Task _authenticated_when_api_resposne_is_ok()
            {
                MockResposneStatusCodeForApiClient(HttpStatusCode.OK);

                var result = await _accountService.LoginUserAsync(new AccountLoginData());

                result.Should().Be(AccountLoginResult.Authenticated);
            }

            [Test]
            public async void Authenticated_and_set_token_when_api_resposne_is_ok()
            {
                var expectedToken = "token";

                MockResposneStatusCodeForApiClient(HttpStatusCode.OK, expectedToken);

                var result = await _accountService.LoginUserAsync(new AccountLoginData());

                result.Should().Be(AccountLoginResult.Authenticated);

                ApiClient.Received().SetAuthToken(expectedToken);
            }
        }

        
    }
}
