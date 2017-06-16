using System;
using System.Collections.Generic;

namespace ReckonMe.Models
{
    public class Expense : BaseDataObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string Payer { get; set; }
        public DateTime Date { get; set; }
        public List<string> Members { get; set; }
    }
}
