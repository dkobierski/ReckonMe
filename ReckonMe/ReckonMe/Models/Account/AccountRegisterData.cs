namespace ReckonMe.Models.Account
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountRegisterData
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
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum AccountRegisterResult
    {
        AccountCreated,
        AlreadyExist,
        RequestException
    }
}