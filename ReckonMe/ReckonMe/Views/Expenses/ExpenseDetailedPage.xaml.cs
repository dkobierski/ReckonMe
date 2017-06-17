using System;
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

        private void OnDelete(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnEdit(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
