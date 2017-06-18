using System;
using ReckonMe.Models.Wallet;
using Xamarin.Forms;

namespace ReckonMe.Views.Wallets
{
    public partial class EditWalletPage : ContentPage
    {
        public Wallet Wallet { get; set; }

        public EditWalletPage(Wallet wallet)
        {
            InitializeComponent();

            Wallet = wallet;

            BindingContext = this;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "EditItem", Wallet);
            await Navigation.PopToRootAsync();
        }
    }
}