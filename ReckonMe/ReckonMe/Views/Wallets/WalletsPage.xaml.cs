using System;
using System.Diagnostics;
using ReckonMe.Models.Wallet;
using ReckonMe.ViewModels;
using ReckonMe.Views.Expenses;
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

        private async void OnMore(object sender, EventArgs e)
        {
            
            try
            {
                var mi = ((MenuItem)sender);

                Navigation.PushAsync(new ExpensesPage(new ExpensesViewModel((Wallet)mi.CommandParameter)));

                OnAppearing();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Exception", ex.Message, "OK");
                Debug.WriteLine(ex.Message);
            }
        }

        private async void OnDelete(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
            try
            {
                var mi = ((MenuItem)sender);

                await _viewModel.DataStore.DeleteItemAsync((Wallet)mi.CommandParameter);
                OnAppearing();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Exception", ex.Message, "OK");
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
