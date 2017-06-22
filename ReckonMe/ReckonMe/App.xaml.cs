using ReckonMe.Services;
using ReckonMe.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LoginPage = ReckonMe.Views.Accounts.LoginPage;
using WalletsPage = ReckonMe.Views.Wallets.WalletsPage;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ReckonMe
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Xamarin.Forms.Application" />
    public partial class App : Application
    {
        /// <summary>
        /// The account service
        /// </summary>
        private readonly IAccountService _accountService;

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App() : this(DependencyService.Get<IAccountService>())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        /// <param name="accountService">The account service.</param>
        public App(IAccountService accountService)
        {
            _accountService = accountService;
            InitializeComponent();

            SetMainPage();
        }

        /// <summary>
        /// Sets the main page.
        /// </summary>
        public void SetMainPage()
        {
            Current.MainPage = _accountService.IsUserLoggedIn() 
                ? new NavigationPage(new WalletsPage()) 
                : new NavigationPage(new LoginPage());
        }
    }
}
