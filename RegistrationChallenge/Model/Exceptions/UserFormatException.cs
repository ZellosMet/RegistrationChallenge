namespace RegistrationChallenge.Model.Exceptions
{
    // UserFormatException - исключение некорректного пользователя
    public class UserFormatException : ApplicationException
    {
        public UserFormatException() : base("user is invalid") { }

        public UserFormatException(string details) : base($"user is invalid: {details}") { }
    }
}
