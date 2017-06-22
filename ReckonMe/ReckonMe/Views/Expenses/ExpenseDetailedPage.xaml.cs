using System;
using ReckonMe.Models;
using ReckonMe.ViewModels;
using Xamarin.Forms;

namespace ReckonMe.Views.Expenses
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public partial class ExpenseDetailedPage : ContentPage
    {
        /// <summary>
        /// The view model
        /// </summary>
        private readonly ExpenseViewModel _viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseDetailedPage"/> class.
        /// </summary>
        public ExpenseDetailedPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseDetailedPage"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public ExpenseDetailedPage(ExpenseViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }

        /// <summary>
        /// Called when [delete].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void OnDelete(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "DeleteItem", _viewModel.Expense.Id);
            await Navigation.PopAsync(true);
        }

        /// <summary>
        /// Called when [edit].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnEdit(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditExpensePage(_viewModel.Expense));
        }
    }
}
