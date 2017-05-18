using ReckonMe.Services;
using ReckonMe.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ReckonMe
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SetMainPage();
        }

        public static void SetMainPage()
        {
            if (DependencyService.Get<IAccountService>().IsUserLoggedIn())
            {
                Current.MainPage = new TabbedPage
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
            }
            else
            {
                Current.MainPage = new NavigationPage(new SingUp());
            }
        }
    }
}
