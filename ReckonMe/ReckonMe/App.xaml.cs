using ReckonMe.Services;
using ReckonMe.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
