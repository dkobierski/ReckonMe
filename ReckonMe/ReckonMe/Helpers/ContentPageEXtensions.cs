using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ReckonMe.Helpers
{
    public static class ContentPageEXtensions
    {
        public static async void ShowErrorMessageIfUnhandledExceptionOccured(this ContentPage page, Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                await page.DisplayAlert("Exception", ex.Message, "OK");
                Debug.WriteLine(ex.Message);
            }
        }
    }
}