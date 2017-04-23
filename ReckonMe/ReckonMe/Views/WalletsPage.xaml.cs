using System;

using ReckonMe.Models;
using ReckonMe.ViewModels;

using Xamarin.Forms;

namespace ReckonMe.Views
{
    public partial class WalletsPage : ContentPage
    {
        WalletsViewModel viewModel;

        public WalletsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new WalletsViewModel();
        }

        async void OnWalletSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var wallet = args.SelectedItem as Wallet;
            if (wallet == null)
                return;

            await Navigation.PushAsync(new WalletDetailPage(new WalletDetailViewModel(wallet)));

            // Manually deselect item
            WalletsListView.SelectedItem = null;
        }

        async void AddWallet_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewWalletPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Wallets.Count == 0)
                viewModel.LoadWalletsCommand.Execute(null);
        }
    }
}
