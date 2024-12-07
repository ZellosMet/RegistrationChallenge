namespace RegistrationChallenge.Model.Exceptions
{
    // DuplicatedDataException - исключение дублирующихся данных
    public class DuplicatedDataException : ApplicationException
    {
        public DuplicatedDataException(string field, string value) : base($"'{field}' is duplicated: '{value}'") { }
    }
}
