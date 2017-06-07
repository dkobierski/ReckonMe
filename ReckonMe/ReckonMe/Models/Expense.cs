using System.Collections.Generic;

namespace ReckonMe.Models
{
    public class Expense : BaseDataObject
    {
        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _description = string.Empty;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private decimal _cost;
        public decimal Cost
        {
            get => _cost;
            set => SetProperty(ref _cost, value);
        }

        private string _payer;
        public string Payer
        {
            get => _payer;
            set => SetProperty(ref _payer, value);
        }

        private IList<string> _members;
        public IList<string> Members
        {
            get => _members;
            set => SetProperty(ref _members, value);
        }
    }
}
