using ReckonMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ReckonMe.Services.MockExpensesDataStore))]
namespace ReckonMe.Services
{
    class MockExpensesDataStore : IDataStore<Expense>
    {
        private bool _isInitialized;
        private List<Expense> _items;

        public async Task<bool> AddItemAsync(Expense item)
        {
            await InitializeAsync();

            _items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Expense item)
        {
            await InitializeAsync();

            var _item = _items.FirstOrDefault(arg => arg.Id == item.Id);
            _items.Remove(_item);
            _items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Expense item)
        {
            await InitializeAsync();

            var _item = _items.FirstOrDefault(arg => arg.Id == item.Id);
            _items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Expense> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(_items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Expense>> GetItemsAsync(bool forceRefresh = false)
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
            if (_isInitialized)
                return;

            _items = new List<Expense>();

            var mockedExpenses = new List<Expense>
            {
                new Expense { Id = Guid.NewGuid().ToString(), Name = "Testowy wydatek", Description="Testowy opis", Cost = 15.25m},
            };

            foreach (var item in mockedExpenses)
            {
                _items.Add(item);
            }

            _isInitialized = true;
        }
    }
}
