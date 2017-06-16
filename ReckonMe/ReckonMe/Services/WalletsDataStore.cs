using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReckonMe.Models;
using ReckonMe.Models.Wallet;
using Xamarin.Forms;

[assembly: Dependency(typeof(ReckonMe.Services.WalletsDataStore))]
namespace ReckonMe.Services
{
    public class WalletsDataStore : IDataStore<Wallet>
    {
        private List<Wallet> _wallets;

        private readonly IWalletService _service;

        public WalletsDataStore() : this(DependencyService.Get<IWalletService>())
        {
        }

        public WalletsDataStore(IWalletService service)
        {
            _service = service;
        }

        public async Task<bool> AddItemAsync(Wallet item)
        {
            await _service.AddWallet(new AddWalletData()
            {
                Description = item.Description,
                Expenses = new List<Expense>(),
                Members = new List<string>(),
                Name = item.Name,
                Owner = "dkobierski"
            });

            _wallets.Add(item);

            return true;
        }

        public async Task<bool> UpdateItemAsync(Wallet item)
        {
            await InitializeAsync();
            
            var index = _wallets.FindIndex(arg => arg.Id == item.Id);

            await _service.UpdateWallet(item.Id, new EditWalletData
            {
                Name = item.Name,
                Expenses = item.Expenses,
                Description = item.Description,
                Members = item.Members,
                Owner = item.Owner
            });

            _wallets[index] = item;

            return true;
        }

        public async Task<bool> DeleteItemAsync(Wallet item)
        {
            await _service.RemoveWallet(item.Id);
            
            _wallets.RemoveAt(_wallets.FindIndex(arg => arg.Id == item.Id));
            
            return true;
        }

        public async Task<Wallet> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(_wallets.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Wallet>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(_wallets);
        }

        public Task<bool> PullLatestAsync()
        {
            return Task.FromResult(true);
        }


        public Task<bool> SyncAsync()
        {
            return Task.FromResult(true);
        }

        public async Task InitializeAsync()
        {
            _wallets = new List<Wallet>();
            _wallets.AddRange(await _service.GetWalletsForUserAsync());
        }
    }
}
