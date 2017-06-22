using System.Collections.Generic;
using System.Linq;

namespace ReckonMe.Models.Wallet
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ReckonMe.Models.BaseDataObject" />
    public class Wallet : BaseDataObject
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
        /// The owner
        /// </summary>
        private string _owner = string.Empty;

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
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public string Owner
        {
            get => _owner;
            set => SetProperty(ref _owner, value);
        }

        /// <summary>
        /// Gets or sets the members.
        /// </summary>
        /// <value>
        /// The members.
        /// </value>
        public List<string> Members { get; set; }
        /// <summary>
        /// Gets or sets the expenses.
        /// </summary>
        /// <value>
        /// The expenses.
        /// </value>
        public List<Expense> Expenses { get; set; } = new List<Expense>();

        /// <summary>
        /// Gets the total sum.
        /// </summary>
        /// <value>
        /// The total sum.
        /// </value>
        public decimal TotalSum => Expenses.Sum(s => s.Value);
    }
}
