using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ReckonMe.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class ContentPageEXtensions
    {
        /// <summary>
        /// Shows the error message if unhandled exception occured.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="action">The action.</param>
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