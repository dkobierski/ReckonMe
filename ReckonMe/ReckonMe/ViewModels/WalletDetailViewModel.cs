using ReckonMe.Helpers;
using ReckonMe.Models;
using ReckonMe.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ReckonMe.ViewModels
{
    public class WalletDetailViewModel : BaseViewModel
    {
        
        public ObservableRangeCollection<Expense> Expenses { get; set; }
        public Command LoadExpensesCommand { get; set; }
        public Wallet Wallet { get; set; }

        public WalletDetailViewModel(Wallet wallet = null)
        {
            Title = wallet.Text;
            Wallet = wallet;
            Expenses = new ObservableRangeCollection<Expense>();
            LoadExpensesCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        int quantity = 1;
        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Expenses.Clear();
                var items = await ExpenseDataStore.GetItemsAsync(true);
                Expenses.ReplaceRange(items);
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