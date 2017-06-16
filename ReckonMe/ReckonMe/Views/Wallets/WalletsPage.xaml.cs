using System;
using ReckonMe.Models.Wallet;
using ReckonMe.ViewModels;
using Xamarin.Forms;

namespace ReckonMe.Views.Wallets
{
    public partial class WalletsPage : ContentPage
    {
        private readonly WalletsViewModel _viewModel;

        public WalletsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new WalletsViewModel();
        }

        private async void OnWalletSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var wallet = args.SelectedItem as Wallet;
            if (wallet == null)
                return;

            
            await Navigation.PushAsync(new Expenses.ExpensesPage(new ExpensesViewModel(wallet)));

            // Manually deselect item
            WalletsListView.SelectedItem = null;
        }

        private async void AddWallet_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewWalletPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.Wallets.Count == 0)
                _viewModel.LoadWalletsCommand.Execute(null);
        }
    }
}
