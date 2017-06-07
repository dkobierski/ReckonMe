using System;
using System.Diagnostics;
using System.Threading.Tasks;

using ReckonMe.Helpers;
using ReckonMe.Models;
using ReckonMe.Models.Wallet;
using ReckonMe.Views;

using Xamarin.Forms;

namespace ReckonMe.ViewModels
{
    public class WalletsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Wallet> Wallets { get; set; }
        public Command LoadWalletsCommand { get; set; }

        public WalletsViewModel()
        {
            Name = "Wallets";          
            Wallets = new ObservableRangeCollection<Wallet>();
            LoadWalletsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewWalletPage, Wallet>(this, "AddItem", async (obj, item) =>
            {
                var wallet = item as Wallet;
                Wallets.Add(wallet);
                await DataStore.AddItemAsync(wallet);
            });
        }

        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Wallets.Clear();
                var items = await DataStore.GetItemsAsync(true);
                Wallets.ReplaceRange(items);
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