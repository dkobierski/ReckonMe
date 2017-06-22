using System;
using System.Diagnostics;
using ReckonMe.Helpers;
using ReckonMe.Models.Wallet;
using ReckonMe.ViewModels;
using ReckonMe.Views.Expenses;
using Xamarin.Forms;

namespace ReckonMe.Views.Wallets
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public partial class WalletsPage : ContentPage
    {
        /// <summary>
        /// The view model
        /// </summary>
        private readonly WalletsViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="WalletsPage"/> class.
        /// </summary>
        public WalletsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new WalletsViewModel();
        }

        /// <summary>
        /// Called when [wallet selected].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="SelectedItemChangedEventArgs"/> instance containing the event data.</param>
        private async void OnWalletSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var wallet = args.SelectedItem as Wallet;
            if (wallet == null)
                return;

            
            await Navigation.PushAsync(new ExpensesPage(new ExpensesViewModel(wallet)));

            // Manually deselect item
            WalletsListView.SelectedItem = null;
        }

        /// <summary>
        /// Handles the Clicked event of the AddWallet control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void AddWallet_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewWalletPage());
        }

        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.Wallets.Count == 0)
                _viewModel.LoadWalletsCommand.Execute(null);
        }

        /// <summary>
        /// Called when [more].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnMore(object sender, EventArgs e)
        {
            this.ShowErrorMessageIfUnhandledExceptionOccured(async () =>
            {
                var mi = ((MenuItem)sender);

                await Navigation.PushAsync(new ExpensesPage(new ExpensesViewModel((Wallet)mi.CommandParameter)));

                OnAppearing();
            });
        }

        /// <summary>
        /// Called when [edit].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnEdit(object sender, EventArgs e)
        {
            this.ShowErrorMessageIfUnhandledExceptionOccured(async () =>
            {
                var mi = (MenuItem)sender;

                await Navigation.PushAsync(new EditWalletPage((Wallet)mi.CommandParameter));
            });
        }

        /// <summary>
        /// Called when [delete].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnDelete(object sender, EventArgs e)
        {

            this.ShowErrorMessageIfUnhandledExceptionOccured(async () =>
            {
                await Navigation.PopAsync(true);
                
                var mi = (MenuItem)sender;

                await _viewModel.DataStore.DeleteItemAsync((Wallet)mi.CommandParameter);
                
                _viewModel.LoadWalletsCommand.Execute(null);
                OnAppearing();
            });
        }
    }
}
