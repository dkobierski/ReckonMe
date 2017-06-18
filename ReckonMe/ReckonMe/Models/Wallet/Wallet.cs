using System.Collections.Generic;
using System.Linq;

namespace ReckonMe.Models.Wallet
{
    public class Wallet : BaseDataObject
    {
        private string _name = string.Empty;
        private string _description = string.Empty;
        private string _owner = string.Empty;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string Owner
        {
            get => _owner;
            set => SetProperty(ref _owner, value);
        }

        public List<string> Members { get; set; }
        public List<Expense> Expenses { get; set; }

        public decimal TotalSum => Expenses.Sum(s => s.Value);
    }
}
