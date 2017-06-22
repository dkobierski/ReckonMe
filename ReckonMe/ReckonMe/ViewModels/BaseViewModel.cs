using ReckonMe.Helpers;
using ReckonMe.Models;
using ReckonMe.Models.Wallet;
using ReckonMe.Services;
using Xamarin.Forms;

namespace ReckonMe.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ReckonMe.Helpers.ObservableObject" />
    public class BaseViewModel : ObservableObject
    {
        /// <summary>
        /// Get the azure service instance
        /// </summary>
        public IDataStore<Wallet> DataStore => DependencyService.Get<IDataStore<Wallet>>();
        /// <summary>
        /// Gets the member data store.
        /// </summary>
        /// <value>
        /// The member data store.
        /// </value>
        public IDataStore<Member> MemberDataStore => DependencyService.Get<IDataStore<Member>>();

        /// <summary>
        /// The is busy
        /// </summary>
        private bool _isBusy;
        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is busy; otherwise, <c>false</c>.
        /// </value>
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
        /// <summary>
        /// Private backing field to hold the title
        /// </summary>
        private string _name = string.Empty;
        /// <summary>
        /// Public property to set and get the title of the item
        /// </summary>
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
    }
}

