namespace RegistrationChallenge.Api.Messages
{
    // описание record-ов для сообщений API

    // StringMessage - record для описания строкового сообщения
    public record StringMessage (string Message);

    // ErrorMessage - record для описания сообщения об ошибке
    public record ErrorMessage(string Type,  string Message);

    // UserRegistrationMessage - record для описания данных для регистрации пользователя
    public record UserRegistrationMessage(string Login, string Email);

    // UserInfoMessage - record для описания данных о пользователей
    public record UserInfoMessage(Guid UUID, string Login, string Email, bool Confirm);
}
