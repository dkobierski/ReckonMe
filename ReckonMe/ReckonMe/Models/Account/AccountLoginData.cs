namespace ReckonMe.Models.Account
{
    public class AccountLoginData
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public enum AccountLoginResult
    {
        Authenticated,
        InvalidCredentials,
        RequestException
    }
}
