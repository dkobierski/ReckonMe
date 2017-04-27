using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ReckonMe.Models;

using Xamarin.Forms;

[assembly: Dependency(typeof(ReckonMe.Services.MockWalletsDataStore))]
namespace ReckonMe.Services
{
    public class MockWalletsDataStore : IDataStore<Wallet>
    {
        bool isInitialized;
        List<Wallet> items;

        public async Task<bool> AddItemAsync(Wallet item)
        {
            await InitializeAsync();

            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Wallet item)
        {
            await InitializeAsync();

            var _item = items.Where((Wallet arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Wallet item)
        {
            await InitializeAsync();

            var _item = items.Where((Wallet arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Wallet> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Wallet>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(items);
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
            if (isInitialized)
                return;

            items = new List<Wallet>();
            var _items = new List<Wallet>
            {
                new Wallet { Id = Guid.NewGuid().ToString(), Text = "Portfel rodzinny", Description="Budżet rodzinny"},
                new Wallet { Id = Guid.NewGuid().ToString(), Text = "Portfel mieszkaniowy", Description="Rozliczenia ze współlokatorami"},
            };

            foreach (Wallet item in _items)
            {
                items.Add(item);
            }

            isInitialized = true;
        }
    }
}
