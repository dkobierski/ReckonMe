using System;
using ReckonMe.Models;
using ReckonMe.Models.Account;
using ReckonMe.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReckonMe.Views
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
            
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var user = new AccountLoginData()
            {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text
            };

            var result = await _accountService.LoginUserAsync(user);

            switch (result)
            {
                case AccountLoginResult.Authenticated:
                    var page = new TabbedPage
                    {
                        Children =
                        {
                            new NavigationPage(new WalletsPage())
                            {
                                Title = "Wallets",
                                Icon = Device.OnPlatform("tab_feed.png", null, null)
                            },
                            new NavigationPage(new AboutPage())
                            {
                                Title = "About",
                                Icon = Device.OnPlatform("tab_about.png", null, null)
                            }
                        }
                    };

                    Navigation.InsertPageBefore(page, this);
                    await Navigation.PopAsync();
                    break;
                case AccountLoginResult.InvalidCredentials:
                    messageLabel.Text = "Invalid Credentials";
                    break;
                case AccountLoginResult.RequestException:
                    messageLabel.Text = "Request failed";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
