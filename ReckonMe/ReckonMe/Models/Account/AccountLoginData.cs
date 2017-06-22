namespace ReckonMe.Models.Account
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountLoginData
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum AccountLoginResult
    {
        Authenticated,
        InvalidCredentials,
        RequestException
    }
}
