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
    public class ExpensesViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Expense> Expenses { get; set; }
        public Command LoadExpensesCommand { get; set; }
        public Wallet Wallet { get; set; }

        public ExpensesViewModel(Wallet wallet)
        {
            Name = wallet?.Name;
            Wallet = wallet;
            Expenses = new ObservableRangeCollection<Expense>(wallet?.Expenses);
            LoadExpensesCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewExpensePage, Expense>(this, "AddItem", async (obj, item) =>
            {
                var expense = item as Expense;

                Expenses.Add(expense);

                await DataStore.UpdateItemAsync(Wallet);

                await ExecuteLoadItemsCommand();
            });
        }

        int _quantity = 1;

        public int Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }

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