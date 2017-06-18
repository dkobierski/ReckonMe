using System;
using System.Collections.Generic;

namespace ReckonMe.Models
{
    public class Expense : BaseDataObject
    {
        private string _name = string.Empty;
        private string _description = string.Empty;
        private decimal _value;
        private string _payer = string.Empty;

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

        public decimal Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        public string Payer
        {
            get => _payer;
            set => SetProperty(ref _payer, value);
        }

        public DateTime Date { get; set; }
        public List<string> Members { get; set; }
    }
}
