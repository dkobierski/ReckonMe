
using System;
using ReckonMe.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(ReckonMe.Helpers.AppStateHolder))]
namespace ReckonMe.Helpers
{
    public class AppStateHolder : IAppStateHolder
    {
        public ApplicationUser User { get; private set; }

        public void SetUser(ApplicationUser user)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
        }
    }
}