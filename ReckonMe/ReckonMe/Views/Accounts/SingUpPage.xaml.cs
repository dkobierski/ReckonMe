using System;
using System.Linq;
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
    public partial class SingUpPage : ContentPage
    {
        /// <summary>
        /// The account service
        /// </summary>
        private readonly IAccountService _accountService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingUpPage"/> class.
        /// </summary>
        public SingUpPage() : this(DependencyService.Get<IAccountService>())
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingUpPage"/> class.
        /// </summary>
        /// <param name="accountService">The account service.</param>
        public SingUpPage(IAccountService accountService)
        {
            _accountService = accountService;
            InitializeComponent();
        }

        /// <summary>
        /// Called when [sign up clicked].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            var user = new AccountRegisterData
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
                    MessageLabel.Text = AccountResponses.UserAlreadyExists;
                    break;
                case AccountRegisterResult.RequestException:
                    MessageLabel.Text = AccountResponses.ConnectionProblem;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}