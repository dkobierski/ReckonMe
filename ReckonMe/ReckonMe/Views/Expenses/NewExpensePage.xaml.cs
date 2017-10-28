using System;
using System.Linq;
using ReckonMe.Helpers;
using ReckonMe.Models;
using ReckonMe.Models.Wallet;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReckonMe.Views.Expenses
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewExpensePage : ContentPage
    {
        private readonly Wallet _wallet;

        public Expense Expense { get; set; }
        public string Members { get; set; }

        public NewExpensePage(Wallet wallet)
        {
            InitializeComponent();

            _wallet = wallet;

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
                Expense.WalletId = _wallet.Id;
                MessagingCenter.Send(this, "AddItem", Expense);
                await Navigation.PopAsync(true);
            });
        }
    }
}