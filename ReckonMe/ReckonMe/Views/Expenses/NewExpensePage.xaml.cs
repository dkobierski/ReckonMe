using System;
using System.Linq;
using ReckonMe.Helpers;
using ReckonMe.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReckonMe.Views.Expenses
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewExpensePage : ContentPage
    {
        public Expense Expense { get; set; }
        public string Members { get; set; }

        public NewExpensePage()
        {
            InitializeComponent();

            Expense = new Expense
            {
                Name = "",
                Description = "",
                Value = 0,
                Payer = ""
            };

            BindingContext = this;
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            this.ShowErrorMessageIfUnhandledExceptionOccured(async () =>
            {
                Expense.Members = Members?.Split(',').Select(m => m.Trim()).ToList();
                if (Expense.Members == null)
                    return;
                MessagingCenter.Send(this, "AddItem", Expense);
                await Navigation.PopAsync(true);
            });
        }
    }
}