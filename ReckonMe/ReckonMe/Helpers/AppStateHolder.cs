
using System;
using ReckonMe.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(ReckonMe.Helpers.AppStateHolder))]
namespace ReckonMe.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ReckonMe.Helpers.IAppStateHolder" />
    public class AppStateHolder : IAppStateHolder
    {
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public ApplicationUser User { get; private set; }

        /// <summary>
        /// Sets the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <exception cref="System.ArgumentNullException">user</exception>
        public void SetUser(ApplicationUser user)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
        }
    }
}