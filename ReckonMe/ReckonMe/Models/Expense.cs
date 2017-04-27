using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReckonMe.Models
{
    public class Expense : BaseDataObject
    {
        string text = string.Empty;
        public string Text
        {
            get { return text; }
            set { SetProperty(ref text, value); }
        }

        string description = string.Empty;
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }
    }
}
