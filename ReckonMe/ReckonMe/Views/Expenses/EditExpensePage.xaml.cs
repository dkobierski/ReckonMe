using System;
using System.Linq;
using System.Text;
using ReckonMe.Helpers;
using ReckonMe.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReckonMe.Views.Expenses
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditExpensePage : ContentPage
    {
        /// <summary>
        /// Gets or sets the expense.
        /// </summary>
        /// <value>
        /// The expense.
        /// </value>
        public Expense Expense { get; set; }
        /// <summary>
        /// Gets or sets the members.
        /// </summary>
        /// <value>
        /// The members.
        /// </value>
        public string Members { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditExpensePage"/> class.
        /// </summary>
        /// <param name="expense">The expense.</param>
        public EditExpensePage(Expense expense)
        {
            InitializeComponent();

            Expense = expense;
            Members = ConcatMembers();

            BindingContext = this;
        }

        /// <summary>
        /// Handles the Clicked event of the Save control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Concats the members.
        /// </summary>
        /// <returns></returns>
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