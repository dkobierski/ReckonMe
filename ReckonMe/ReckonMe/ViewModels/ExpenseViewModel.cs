using System.Linq;
using ReckonMe.Models;
using System.Text;
using ReckonMe.Models.Wallet;
using ReckonMe.Views;
using ReckonMe.Views.Expenses;
using Xamarin.Forms;

namespace ReckonMe.ViewModels
{
    public class ExpenseViewModel : BaseViewModel
    {
        private string _members = string.Empty;
        private string _costForMember = string.Empty;

        public Expense Expense { get; set; }

        public string Members
        {
            get => _members;
            set => SetProperty(ref _members, value);
        }

        public string CostForMember
        {
            get => _costForMember;
            set => SetProperty(ref _costForMember, value);
        }

        public ExpenseViewModel(Expense expense = null)
        {
            Name = expense?.Name;
            Expense = expense;
            Members = ConcatMembers();
            CostForMember = CalculateCostForMember();

            MessagingCenter.Subscribe<EditExpensePage, Expense>(this, "EditItem", (page, item) =>
            {
                Members = ConcatMembers();
                CostForMember = CalculateCostForMember();
            });
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
            var dividedCost = Expense.Value / membersCount;
            return dividedCost.ToString("#.##");
        }

        public string ConcatMembers()
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