using ReckonMe.Services;
using ReckonMe.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LoginPage = ReckonMe.Views.Accounts.LoginPage;
using WalletsPage = ReckonMe.Views.Wallets.WalletsPage;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ReckonMe
{
    public partial class App : Application
    {
        private readonly IAccountService _accountService;

        public App() : this(DependencyService.Get<IAccountService>())
        { }

        public App(IAccountService accountService)
        {
            _accountService = accountService;
            InitializeComponent();

            SetMainPage();
        }

        public void SetMainPage()
        {
            Current.MainPage = _accountService.IsUserLoggedIn() 
                ? new NavigationPage(new WalletsPage()) 
                : new NavigationPage(new LoginPage());
        }
    }
}
