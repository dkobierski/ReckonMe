using ReckonMe.Models;


namespace ReckonMe.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAppStateHolder
    {
        /// <summary>
        /// Sets the user.
        /// </summary>
        /// <param name="user">The user.</param>
        void SetUser(ApplicationUser user);
    }
}