using System;
using System.Diagnostics;
using ReckonMe.Models;
using ReckonMe.ViewModels;
using ReckonMe.Views.Wallets;
using Xamarin.Forms;

namespace ReckonMe.Views.Expenses
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public partial class ExpensesPage : ContentPage
    {
        /// <summary>
        /// The view model
        /// </summary>
        private readonly ExpensesViewModel _viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpensesPage"/> class.
        /// </summary>
        public ExpensesPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpensesPage"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public ExpensesPage(ExpensesViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }

        /// <summary>
        /// Called when [expense selected].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="SelectedItemChangedEventArgs"/> instance containing the event data.</param>
        private async void OnExpenseSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var expense = args.SelectedItem as Expense;
            if (expense == null)
                return;

            await Navigation.PushAsync(new ExpenseDetailedPage(new ExpenseViewModel(expense)));
            // Manually deselect item
            ExpensesListView.SelectedItem = null;
        }

        /// <summary>
        /// Handles the Clicked event of the AddExpense control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void AddExpense_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewExpensePage(_viewModel.Wallet));
        }

        /// <summary>
        /// Handles the Clicked event of the Edit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditWalletPage(_viewModel.Wallet));
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

            if (_viewModel.Expenses.Count == 0)
                _viewModel.LoadExpensesCommand.Execute(null);
        }

        /// <summary>
        /// Called when [delete].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void OnDelete(object sender, EventArgs e)
        {
            try
            {
                var mi = ((MenuItem)sender);

                MessagingCenter.Send(this, "DeleteItem", ((Expense)mi.CommandParameter).Id);

                OnAppearing();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Exception", ex.Message, "OK");
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Called when [more].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnMore(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);

            Navigation.PushAsync(
                new ExpenseDetailedPage(
                    new ExpenseViewModel((Expense)mi.CommandParameter)),
                    true
                    );
        }

        /// <summary>
        /// Called when [edit].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnEdit(object sender, EventArgs e)
        {
            var mi = (MenuItem) sender;

            Navigation.PushAsync(new EditExpensePage((Expense) mi.CommandParameter));
        }
    }
}
