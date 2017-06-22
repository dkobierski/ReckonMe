using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReckonMe.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataStore<T>
    {
        /// <summary>
        /// Adds the item asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        Task<bool> AddItemAsync(T item);
        /// <summary>
        /// Updates the item asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        Task<bool> UpdateItemAsync(T item);
        /// <summary>
        /// Deletes the item asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        Task<bool> DeleteItemAsync(T item);
        /// <summary>
        /// Gets the item asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<T> GetItemAsync(string id);
        /// <summary>
        /// Gets the items asynchronous.
        /// </summary>
        /// <param name="forceRefresh">if set to <c>true</c> [force refresh].</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);

        /// <summary>
        /// Initializes the asynchronous.
        /// </summary>
        /// <returns></returns>
        Task InitializeAsync();
        /// <summary>
        /// Pulls the latest asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<bool> PullLatestAsync();
        /// <summary>
        /// Synchronizes the asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<bool> SyncAsync();
    }
}
