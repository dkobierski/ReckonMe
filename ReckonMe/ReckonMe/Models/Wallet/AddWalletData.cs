using System.Collections.Generic;

namespace ReckonMe.Models.Wallet
{
    public class AddWalletData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public IEnumerable<string> Members { get; set; }
        public IEnumerable<Expense> Expenses { get; set; }
    }

    public class EditWalletData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public IEnumerable<string> Members { get; set; }
        public IEnumerable<Expense> Expenses { get; set; }
    }
}