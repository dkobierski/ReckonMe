using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using ReckonMe.Helpers;
using ReckonMe.Models.Account;
using ReckonMe.Models.Security;
using ReckonMe.Services;
using Xunit;

namespace ReckonMe.Tests
{

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


            [Fact]
            public void Authenticated_when_api_resposne_is_ok()
            {
                //            var task = Task.FromResult(_authenticated_when_api_resposne_is_ok);
                //            task.Wait();
            }

            [Fact]
            public void _authenticated_when_api_resposne_is_ok()
            {
                MockResposneStatusCodeForApiClient(HttpStatusCode.OK);

                var result = Task.FromResult(_accountService.LoginUserAsync(new AccountLoginData()));

                result.Should().Be(AccountLoginResult.Authenticated);
            }

            [Fact]
            public async void set_token_when_api_resposne_is_ok()
            {
                var expectedToken = "token";

                MockResposneStatusCodeForApiClient(HttpStatusCode.OK, expectedToken);

                var result = await _accountService.LoginUserAsync(new AccountLoginData());
                
                ApiClient.Received().SetAuthToken(expectedToken);
            }
        }




    }

    
}
