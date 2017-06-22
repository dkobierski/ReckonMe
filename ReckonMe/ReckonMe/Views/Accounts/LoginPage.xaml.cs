using System;
using ReckonMe.Constants;
using ReckonMe.Models.Account;
using ReckonMe.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReckonMe.Views.Accounts
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        /// <summary>
        /// The account service
        /// </summary>
        private readonly IAccountService _accountService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage"/> class.
        /// </summary>
        public LoginPage() : this(DependencyService.Get<IAccountService>())
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage"/> class.
        /// </summary>
        /// <param name="accountService">The account service.</param>
        public LoginPage(IAccountService accountService)
        {
            _accountService = accountService;
            InitializeComponent();
        }

        /// <summary>
        /// Called when [sign up clicked].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SingUpPage());
        }

        /// <summary>
        /// Called when [login clicked].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
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
