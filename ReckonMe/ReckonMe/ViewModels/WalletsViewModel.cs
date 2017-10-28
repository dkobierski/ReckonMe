using System;
using System.Diagnostics;
using System.Threading.Tasks;

using ReckonMe.Helpers;
using ReckonMe.Models.Wallet;
using ReckonMe.Views.Wallets;
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

            MessagingCenter.Subscribe<NewWalletPage, Wallet>(this, "AddItem", async (page, wallet) =>
            {
                Wallets.Add(wallet);
                await DataStore.AddItemAsync(wallet);
            });

            MessagingCenter.Subscribe<EditWalletPage, Wallet>(this, "EditItem", async (page, wallet) =>
            {
                await DataStore.UpdateItemAsync(wallet);
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