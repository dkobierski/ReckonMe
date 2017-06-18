using System;
using System.Diagnostics;
using ReckonMe.Models;
using ReckonMe.ViewModels;
using ReckonMe.Views.Wallets;
using Xamarin.Forms;

namespace ReckonMe.Views.Expenses
{
    public partial class ExpensesPage : ContentPage
    {
        private readonly ExpensesViewModel _viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public ExpensesPage()
        {
            InitializeComponent();
        }

        public ExpensesPage(ExpensesViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }

        private async void OnExpenseSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var expense = args.SelectedItem as Expense;
            if (expense == null)
                return;

            await Navigation.PushAsync(new ExpenseDetailedPage(new ExpenseViewModel(expense)));
            // Manually deselect item
            ExpensesListView.SelectedItem = null;
        }

        private async void AddExpense_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewExpensePage());
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditWalletPage(_viewModel.Wallet));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.Expenses.Count == 0)
                _viewModel.LoadExpensesCommand.Execute(null);
        }
        
        private async void OnDelete(object sender, EventArgs e)
        {
            try
            {
                var mi = ((MenuItem)sender);

                _viewModel.Wallet.Expenses.Remove((Expense)mi.CommandParameter);

                await _viewModel.DataStore.UpdateItemAsync(_viewModel.Wallet);
                _viewModel.LoadExpensesCommand.Execute(null);

                OnAppearing();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Exception", ex.Message, "OK");
                Debug.WriteLine(ex.Message);
            }
        }

        private void OnMore(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);

            Navigation.PushAsync(
                new ExpenseDetailedPage(
                    new ExpenseViewModel((Expense)mi.CommandParameter)),
                    true
                    );
        }
    }
}
