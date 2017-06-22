using System;
using System.Collections.Generic;

namespace ReckonMe.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ReckonMe.Models.BaseDataObject" />
    public class Expense : BaseDataObject
    {
        /// <summary>
        /// The name
        /// </summary>
        private string _name = string.Empty;
        /// <summary>
        /// The description
        /// </summary>
        private string _description = string.Empty;
        /// <summary>
        /// The value
        /// </summary>
        private decimal _value;
        /// <summary>
        /// The payer
        /// </summary>
        private string _payer = string.Empty;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public decimal Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        /// <summary>
        /// Gets or sets the payer.
        /// </summary>
        /// <value>
        /// The payer.
        /// </value>
        public string Payer
        {
            get => _payer;
            set => SetProperty(ref _payer, value);
        }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date { get; set; }
        /// <summary>
        /// Gets or sets the members.
        /// </summary>
        /// <value>
        /// The members.
        /// </value>
        public List<string> Members { get; set; }

        /// <summary>
        /// Gets or sets the wallet identifier.
        /// </summary>
        /// <value>
        /// The wallet identifier.
        /// </value>
        public string WalletId { get; set; }
    }
}
