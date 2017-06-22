using System;
using ReckonMe.Models.Wallet;
using Xamarin.Forms;

namespace ReckonMe.Views.Wallets
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public partial class NewWalletPage : ContentPage
    {
        /// <summary>
        /// Gets or sets the wallet.
        /// </summary>
        /// <value>
        /// The wallet.
        /// </value>
        public Wallet Wallet { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewWalletPage"/> class.
        /// </summary>
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

        /// <summary>
        /// Handles the Clicked event of the Save control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Wallet);
            await Navigation.PopToRootAsync();
        }
    }
}