
using ReckonMe.ViewModels;

using Xamarin.Forms;

namespace ReckonMe.Views
{
    public partial class WalletDetailPage : ContentPage
    {
        WalletDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public WalletDetailPage()
        {
            InitializeComponent();
        }

        public WalletDetailPage(WalletDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
    }
}
