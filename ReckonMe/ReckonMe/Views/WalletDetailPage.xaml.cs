
using ReckonMe.Models;
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

        async void OnExpenseSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var expense = args.SelectedItem as Expense;
            if (expense == null)
                return;

            //await Navigation.PushAsync(new WalletDetailPage(new WalletDetailViewModel(wallet)));
            await DisplayAlert(expense.Text, expense.Description, "cancel");
            // Manually deselect item
            ExpensesListView.SelectedItem = null;
        }

        async void AddExpense_Clicked(object sender, System.EventArgs e)
        {
            await DisplayAlert("Not implemented", ":)", "cancel");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Expenses.Count == 0)
                viewModel.LoadExpensesCommand.Execute(null);
        }
    }
}
