using ReckonMe.Models;

namespace ReckonMe.ViewModels
{
    public class WalletDetailViewModel : BaseViewModel
    {
        public Wallet Wallet { get; set; }
        public WalletDetailViewModel(Wallet wallet = null)
        {
            Title = wallet.Text;
            Wallet = wallet;
        }

        int quantity = 1;
        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }
    }
}