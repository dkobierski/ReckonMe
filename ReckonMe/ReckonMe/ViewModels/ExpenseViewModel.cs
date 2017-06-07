using ReckonMe.Helpers;
using ReckonMe.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ReckonMe.ViewModels
{
    public class ExpenseViewModel : BaseViewModel
    {
        public Expense Expense { get; set; }
        public string Members { get; set; }
        public string CostForMember { get; set; }

        public ExpenseViewModel(Expense expense = null)
        {
            Name = expense?.Name;
            Expense = expense;
            Members = ConcatMembers();
            CostForMember = CalculateCostForMember();
        }

        private string CalculateCostForMember()
        {
            var membersCount = Expense.Members.Contains(Expense.Payer)
                ? Expense.Members.Count
                : Expense.Members.Count + 1;
            if (membersCount == 0)
            {
                return "0";
            }
            var dividedCost = Expense.Cost / membersCount;
            return dividedCost.ToString("#.##");
        }

        private string ConcatMembers()
        {
            var members = Expense.Members;
            var sb = new StringBuilder();
            foreach (var member in members)
            {
                sb.Append($"{member}, ");
            }
            sb.Length -= 2;

            return sb.ToString();
        }
    }
}