using ReckonMe.Helpers;
using ReckonMe.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ReckonMe.Models.Wallet;
using ReckonMe.Views.Expenses;
using Xamarin.Forms;

namespace ReckonMe.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ReckonMe.ViewModels.BaseViewModel" />
    public class ExpensesViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets or sets the expenses.
        /// </summary>
        /// <value>
        /// The expenses.
        /// </value>
        public ObservableRangeCollection<Expense> Expenses { get; set; }
        /// <summary>
        /// Gets or sets the load expenses command.
        /// </summary>
        /// <value>
        /// The load expenses command.
        /// </value>
        public Command LoadExpensesCommand { get; set; }
        /// <summary>
        /// Gets or sets the wallet.
        /// </summary>
        /// <value>
        /// The wallet.
        /// </value>
        public Wallet Wallet { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpensesViewModel"/> class.
        /// </summary>
        /// <param name="wallet">The wallet.</param>
        public ExpensesViewModel(Wallet wallet)
        {
            Name = wallet?.Name;
            Wallet = wallet;
            Expenses = new ObservableRangeCollection<Expense>(wallet?.Expenses);
            LoadExpensesCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewExpensePage, Expense>(this, "AddItem", async (obj, item) =>
            {
                if (item.WalletId.Equals(Wallet.Id))
                {
                    var expense = item as Expense;

                    Wallet.Expenses.Add(expense);
                    Expenses.ReplaceRange(Wallet.Expenses);

                    await DataStore.UpdateItemAsync(Wallet);
                }
            });

            MessagingCenter.Subscribe<EditExpensePage, Expense>(this, "EditItem", async (page, expense) =>
            {
                var index = Wallet.Expenses.FindIndex(e => e.Id == expense.Id);
                if (index != -1)
                {
                    Wallet.Expenses[index] = expense;
                }
                
                await DataStore.UpdateItemAsync(Wallet);
            });

            MessagingCenter.Subscribe<ExpenseDetailedPage, string>(this, "DeleteItem", async (page, expenseId) =>
            {
                await DeleteExpense(expenseId);
            });

            MessagingCenter.Subscribe<ExpensesPage, string>(this, "DeleteItem", async (page, expenseId) =>
            {
                await DeleteExpense(expenseId);
            });
        }

        /// <summary>
        /// Deletes the expense.
        /// </summary>
        /// <param name="expenseId">The expense identifier.</param>
        /// <returns></returns>
        private async Task DeleteExpense(string expenseId)
        {
            var expense = Wallet.Expenses.Find(e => e.Id == expenseId);
            Wallet.Expenses.Remove(expense);
            await DataStore.UpdateItemAsync(Wallet);
            LoadExpensesCommand.Execute(null);
        }

        /// <summary>
        /// Executes the load items command.
        /// </summary>
        /// <returns></returns>
        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Expenses.Clear();
                var wallet = await DataStore.GetItemAsync(Wallet.Id);
                Expenses.ReplaceRange(wallet.Expenses);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}