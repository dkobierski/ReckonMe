using System;
using ReckonMe.Models.Wallet;
using Xamarin.Forms;

namespace ReckonMe.Views.Wallets
{
    public partial class NewWalletPage : ContentPage
    {
        public Wallet Wallet { get; set; }

        public NewWalletPage()
        {
            InitializeComponent();

            Wallet = new Wallet
            {
                Name = "",
                Description = ""
            };

            BindingContext = this;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Wallet);
            await Navigation.PopToRootAsync();
        }
    }
}