using System;
using ReckonMe.Models;
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
            InitializeComponent();
        }

        public LoginPage(IAccountService accountService)
        {
            _accountService = accountService;
            InitializeComponent();
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var user = new ApplicationUser()
            {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text
            };

            var result = await _accountService.LoginUserAsync(user);

            if (result)
            {
                var page = new TabbedPage
                {
                    Children =
                    {
                        new NavigationPage(new WalletsPage())
                        {
                            Title = "Wallets",
                            Icon = Device.OnPlatform("tab_feed.png",null,null)

                        },
                        new NavigationPage(new AboutPage())
                        {
                            Title = "About",
                            Icon = Device.OnPlatform("tab_about.png",null,null)
                        }
                    }
                };
                
                Navigation.InsertPageBefore(page, this);
                await Navigation.PopAsync();
            }
            else
            {
                messageLabel.Text = "Login failed";
            }
        }
    }
}
