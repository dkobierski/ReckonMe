using ReckonMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ReckonMe.Services.MockExpensesDataStore))]
namespace ReckonMe.Services
{
    class MockExpensesDataStore : IDataStore<Expense>
    {
        bool isInitialized;
        List<Expense> items;

        public async Task<bool> AddItemAsync(Expense item)
        {
            await InitializeAsync();

            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Expense item)
        {
            await InitializeAsync();

            var _item = items.Where((Expense arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Expense item)
        {
            await InitializeAsync();

            var _item = items.Where((Expense arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Expense> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Expense>> GetItemsAsync(bool forceRefresh = false)
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

            items = new List<Expense>();
            var _items = new List<Expense>
            {
                new Expense { Id = Guid.NewGuid().ToString(), Text = "Testowy wydatek", Description="Testowy opis"},
            };

            foreach (Expense item in _items)
            {
                items.Add(item);
            }

            isInitialized = true;
        }
    }
}
