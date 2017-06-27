using ReckonMe.ViewModels;
using Xamarin.Forms;

namespace ReckonMe.Views.Expenses
{
    class ExpensesSummaryPage : ContentPage
    {
        public ExpensesSummaryPage(ExpensesViewModel viewModel)
        {
            Title = "Podsumowanie";

            var listView = new ListView()
            {

            };

            listView.ItemTemplate = new DataTemplate()
            {
                
            };

            Content = new StackLayout()
            {
                Children =
                {
                    new Label()
                    {
                        FontSize = 48,
                        Text = "Bilans",
                        HorizontalTextAlignment = TextAlignment.Center
                    },
                    
                }
            };
        }
    }
}
