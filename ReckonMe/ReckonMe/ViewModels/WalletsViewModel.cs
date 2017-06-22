using System;
using System.Diagnostics;
using System.Threading.Tasks;

using ReckonMe.Helpers;
using ReckonMe.Models.Wallet;
using ReckonMe.Views.Wallets;
using Xamarin.Forms;

namespace ReckonMe.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ReckonMe.ViewModels.BaseViewModel" />
    public class WalletsViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets or sets the wallets.
        /// </summary>
        /// <value>
        /// The wallets.
        /// </value>
        public ObservableRangeCollection<Wallet> Wallets { get; set; }
        /// <summary>
        /// Gets or sets the load wallets command.
        /// </summary>
        /// <value>
        /// The load wallets command.
        /// </value>
        public Command LoadWalletsCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WalletsViewModel"/> class.
        /// </summary>
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