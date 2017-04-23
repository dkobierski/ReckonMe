using ReckonMe.Helpers;
using ReckonMe.Models;
using ReckonMe.Services;

using Xamarin.Forms;

namespace ReckonMe.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        /// <summary>
        /// Get the azure service instance
        /// </summary>
        public IDataStore<Wallet> DataStore => DependencyService.Get<IDataStore<Wallet>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        /// <summary>
        /// Private backing field to hold the title
        /// </summary>
        string title = string.Empty;
        /// <summary>
        /// Public property to set and get the title of the item
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
    }
}

