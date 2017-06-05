using ReckonMe.Models;


namespace ReckonMe.Helpers
{
    public interface IAppStateHolder
    {
        void SetUser(ApplicationUser user);
    }
}