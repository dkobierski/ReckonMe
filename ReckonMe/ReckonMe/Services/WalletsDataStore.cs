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
        private bool _isInitialized;
        private List<Wallet> _items;

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
                Name = item.Text,
                Owner = "dkobierski"
            });

            await InitializeAsync();

            _items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Wallet item)
        {
            await InitializeAsync();

            var _item = _items.FirstOrDefault(arg => arg.Id == item.Id);
            _items.Remove(_item);
            _items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Wallet item)
        {
            await InitializeAsync();

            var _item = _items.FirstOrDefault(arg => arg.Id == item.Id);
            _items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Wallet> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(_items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Wallet>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(_items);
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
//            if (_isInitialized)
//                return;

            _items = new List<Wallet>();

            _items.AddRange(await _service.GetWalletsForUserAsync());
            

//            _isInitialized = true;
        }
    }
}
