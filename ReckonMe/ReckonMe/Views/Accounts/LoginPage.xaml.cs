using System;
using ReckonMe.Constants;
using ReckonMe.Models.Account;
using ReckonMe.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReckonMe.Views.Accounts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly IAccountService _accountService;

        public LoginPage() : this(DependencyService.Get<IAccountService>())
        {
            
        }

        public LoginPage(IAccountService accountService)
        {
            _accountService = accountService;
            InitializeComponent();
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SingUpPage());
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var user = new AccountLoginData
            {
                Username = UsernameEntry.Text,
                Password = PasswordEntry.Text
            };

            var result = await _accountService.LoginUserAsync(user);

            switch (result)
            {
                case AccountLoginResult.Authenticated:
                    Navigation.InsertPageBefore(new Wallets.WalletsPage(), this);
                    await Navigation.PopAsync();
                    break;
                case AccountLoginResult.InvalidCredentials:
                    MessageLabel.Text = AccountResponses.InvalidCredentials;
                    break;
                case AccountLoginResult.RequestException:
                    MessageLabel.Text = AccountResponses.ConnectionProblem;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
