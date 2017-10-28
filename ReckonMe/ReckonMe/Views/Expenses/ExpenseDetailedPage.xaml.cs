using System;
using ReckonMe.Models;
using ReckonMe.ViewModels;
using Xamarin.Forms;

namespace ReckonMe.Views.Expenses
{
    public partial class ExpenseDetailedPage : ContentPage
    {
        private readonly ExpenseViewModel _viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public ExpenseDetailedPage()
        {
            InitializeComponent();
        }

        public ExpenseDetailedPage(ExpenseViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }

        private async void OnDelete(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "DeleteItem", _viewModel.Expense.Id);
            await Navigation.PopAsync(true);
        }

        private void OnEdit(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditExpensePage(_viewModel.Expense));
        }
    }
}
