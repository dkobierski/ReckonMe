namespace ReckonMe.Constants
{
    public static class AccountResponses
    {
        public const string UserAlreadyExists = "Użytkownik o takiej nazwie istnieje już w naszej bazie.";
            //"Given username is already taken by another user.";

        public const string ConnectionProblem = "Wystąpił problem podczas połączenia z serwerem. Porsimy spróbuj ponownie później.";
            //"There was a problem with connection to authentication server.";

        public const string InvalidCredentials = "Padane hasło nie jes właściwe"; 
            //"Invalid credentials.";
    }
}