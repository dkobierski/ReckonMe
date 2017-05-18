using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReckonMe.Models.Account;
using ReckonMe.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReckonMe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingUpPage : ContentPage
    {
        private readonly IAccountService _accountService;

        public SingUpPage() : this(DependencyService.Get<IAccountService>())
        {

        }

        public SingUpPage(IAccountService accountService)
        {
            _accountService = accountService;
            InitializeComponent();
        }

        async void OnSignUpClicked(object sender, EventArgs e)
        {
            var user = new AccountRegisterData()
            {
                Username = UsernameEntry.Text,
                Password = PasswordEntry.Text,
                Email = EmailEntry.Text
            };

            var result = await _accountService.SignUpUserAsync(user);

            switch (result)
            {
                case AccountRegisterResult.AccountCreated:
                    Navigation.InsertPageBefore(new LoginPage(), Navigation.NavigationStack.First());
                    await Navigation.PopToRootAsync();
                    break;
                case AccountRegisterResult.AlreadyExist:
                    MessageLabel.Text = "Sign up failed";
                    break;
                case AccountRegisterResult.RequestException:
                    MessageLabel.Text = "Sign up failed";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}