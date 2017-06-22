using System;
using System.Linq;
using ReckonMe.Helpers;
using ReckonMe.Models;
using ReckonMe.Models.Wallet;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReckonMe.Views.Expenses
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewExpensePage : ContentPage
    {
        /// <summary>
        /// The wallet
        /// </summary>
        private readonly Wallet _wallet;

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
        /// Initializes a new instance of the <see cref="NewExpensePage"/> class.
        /// </summary>
        /// <param name="wallet">The wallet.</param>
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
                Expense.WalletId = _wallet.Id;
                MessagingCenter.Send(this, "AddItem", Expense);
                await Navigation.PopAsync(true);
            });
        }
    }
}