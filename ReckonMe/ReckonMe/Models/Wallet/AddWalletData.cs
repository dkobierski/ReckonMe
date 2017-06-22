using System.Collections.Generic;

namespace ReckonMe.Models.Wallet
{
    /// <summary>
    /// 
    /// </summary>
    public class AddWalletData
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the members.
        /// </summary>
        /// <value>
        /// The members.
        /// </value>
        public IEnumerable<string> Members { get; set; }
        /// <summary>
        /// Gets or sets the expenses.
        /// </summary>
        /// <value>
        /// The expenses.
        /// </value>
        public IEnumerable<Expense> Expenses { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class EditWalletData
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public string Owner { get; set; }
        /// <summary>
        /// Gets or sets the members.
        /// </summary>
        /// <value>
        /// The members.
        /// </value>
        public IEnumerable<string> Members { get; set; }
        /// <summary>
        /// Gets or sets the expenses.
        /// </summary>
        /// <value>
        /// The expenses.
        /// </value>
        public IEnumerable<Expense> Expenses { get; set; }
    }
}