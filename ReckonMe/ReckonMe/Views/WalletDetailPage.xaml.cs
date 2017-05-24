
using ReckonMe.Models;
using ReckonMe.ViewModels;

using Xamarin.Forms;

namespace ReckonMe.Views
{
    public partial class WalletDetailPage : ContentPage
    {
        private readonly WalletDetailViewModel _viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public WalletDetailPage()
        {
            InitializeComponent();
        }

        public WalletDetailPage(WalletDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }

        private async void OnExpenseSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var expense = args.SelectedItem as Expense;
            if (expense == null)
                return;

            //await Navigation.PushAsync(new WalletDetailPage(new WalletDetailViewModel(wallet)));
            await DisplayAlert(expense.Text, expense.Description, "cancel");
            // Manually deselect item
            ExpensesListView.SelectedItem = null;
        }

        private async void AddExpense_Clicked(object sender, System.EventArgs e)
        {
            await DisplayAlert("Not implemented", ":)", "cancel");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.Expenses.Count == 0)
                _viewModel.LoadExpensesCommand.Execute(null);
        }
    }
}
