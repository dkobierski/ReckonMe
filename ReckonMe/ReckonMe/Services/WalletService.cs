using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ReckonMe.Helpers;
using ReckonMe.Models.Wallet;
using Xamarin.Forms;

[assembly: Dependency(typeof(ReckonMe.Services.WalletService))]

namespace ReckonMe.Services
{
    public class WalletService : IWalletService
    {
        private readonly IRequstExecutor _api;

        public WalletService() : this(DependencyService.Get<IRequstExecutor>())
        {
        }

        public WalletService(IRequstExecutor api)
        {
            _api = api;
        }


        public async Task<IEnumerable<Wallet>> GetWalletsForUserAsync()
        {
            try
            {
                var response = await _api.GetAsync("wallets/")
                    .ConfigureAwait(false);
                
                return await ResponseDecoder.Decode<IEnumerable<Wallet>>(response);
            }
            catch (HttpRequestException)
            {
                return Enumerable.Empty<Wallet>();
            }
        }

        public Task<Wallet> GetWalletAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task AddWallet(AddWalletData wallet)
        {
            try
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(wallet),
                    Encoding.UTF8,
                    "application/json");

                var response = await _api.PostAsync("wallets/", content)
                    .ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                }

            }
            catch (Exception)
            {

            }
        }

        public async Task UpdateWallet(string id, EditWalletData wallet)
        {
            try
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(wallet),
                    Encoding.UTF8,
                    "application/json");

                var response = await _api.PutAsync($"wallets/{id}", content)
                    .ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                }
            }
            catch (Exception)
            {

            }
        }

        public async Task RemoveWallet(string id)
        {
            try
            {
                var response = await _api.DeleteAsync($"wallets/{id}")
                    .ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                }
            }
            catch (Exception)
            {

            }
        }
    }
}