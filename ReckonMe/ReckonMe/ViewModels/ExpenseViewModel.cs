using System.Linq;
using ReckonMe.Models;
using System.Text;
using ReckonMe.Models.Wallet;
using ReckonMe.Views;
using ReckonMe.Views.Expenses;
using Xamarin.Forms;

namespace ReckonMe.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ReckonMe.ViewModels.BaseViewModel" />
    public class ExpenseViewModel : BaseViewModel
    {
        /// <summary>
        /// The members
        /// </summary>
        private string _members = string.Empty;
        /// <summary>
        /// The cost for member
        /// </summary>
        private string _costForMember = string.Empty;

        /// <summary>
        /// Gets or sets the expense.
        /// </summary>
        /// <value>
        /// The expense.
        /// </value>
        public Expense Expense { get; set; }

        /// <summary>
        /// Gets or sets the members.
        /// </summary>
        /// <value>
        /// The members.
        /// </value>
        public string Members
        {
            get => _members;
            set => SetProperty(ref _members, value);
        }

        /// <summary>
        /// Gets or sets the cost for member.
        /// </summary>
        /// <value>
        /// The cost for member.
        /// </value>
        public string CostForMember
        {
            get => _costForMember;
            set => SetProperty(ref _costForMember, value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseViewModel"/> class.
        /// </summary>
        /// <param name="expense">The expense.</param>
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

        /// <summary>
        /// Calculates the cost for member.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Concats the members.
        /// </summary>
        /// <returns></returns>
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