using System;
using System.Linq;
using System.Text;
using ReckonMe.Helpers;
using ReckonMe.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReckonMe.Views.Expenses
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditExpensePage : ContentPage
    {
        public Expense Expense { get; set; }
        public string Members { get; set; }

        public EditExpensePage(Expense expense)
        {
            InitializeComponent();

            Expense = expense;
            Members = ConcatMembers();

            BindingContext = this;
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            this.ShowErrorMessageIfUnhandledExceptionOccured(async () =>
            {
                Expense.Members = Members?.Split(',').Select(m => m.Trim()).ToList();
                if (Expense.Members == null)
                    return;
                MessagingCenter.Send(this, "EditItem", Expense);
                await Navigation.PopAsync(true);
            });
        }

        private string ConcatMembers()
        {
            var members = Expense.Members;
            var sb = new StringBuilder();
            foreach (var member in members)
            {
                sb.Append($"{member}, ");
            }
            sb.Length -= 2;

            return sb.ToString();
        }
    }
}