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
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ReckonMe.Services.IWalletService" />
    public class WalletService : IWalletService
    {
        /// <summary>
        /// The API
        /// </summary>
        private readonly IRequstExecutor _api;

        /// <summary>
        /// Initializes a new instance of the <see cref="WalletService"/> class.
        /// </summary>
        public WalletService() : this(DependencyService.Get<IRequstExecutor>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WalletService"/> class.
        /// </summary>
        /// <param name="api">The API.</param>
        public WalletService(IRequstExecutor api)
        {
            _api = api;
        }


        /// <summary>
        /// Gets the wallets for user asynchronous.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the wallet asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<Wallet> GetWalletAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Adds the wallet.
        /// </summary>
        /// <param name="wallet">The wallet.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Updates the wallet.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="wallet">The wallet.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Removes the wallet.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
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