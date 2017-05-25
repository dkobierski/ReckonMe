
using System;
using ReckonMe.Models;
using ReckonMe.ViewModels;

using Xamarin.Forms;

namespace ReckonMe.Views
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

        private async void OnMembersClicked(object sender, EventArgs e)
        {
             await DisplayAlert("Not implemented", ":)", "cancel");
        }
    }
}
