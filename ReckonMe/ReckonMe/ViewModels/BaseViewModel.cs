using ReckonMe.Helpers;
using ReckonMe.Models;
using ReckonMe.Models.Wallet;
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
        public IDataStore<Expense> ExpenseDataStore => DependencyService.Get<IDataStore<Expense>>();
        public IDataStore<Member> MemberDataStore => DependencyService.Get<IDataStore<Member>>();

        private bool _isBusy;
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

