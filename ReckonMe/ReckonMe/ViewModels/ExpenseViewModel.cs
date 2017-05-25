using ReckonMe.Helpers;
using ReckonMe.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ReckonMe.ViewModels
{
    public class ExpenseViewModel : BaseViewModel
    {
        public Expense Expense { get; set; }

        public ExpenseViewModel(Expense expense = null)
        {
            Title = expense?.Text;
            Expense = expense;
        }
    }
}