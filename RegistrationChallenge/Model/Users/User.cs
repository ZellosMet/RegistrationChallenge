namespace RegistrationChallenge.Model.Users
{
    // User - сущность пользователя
    public class User
    {
        // uuid пользователя
        public Guid UUID { get; set; }

        // логин пользователя
        public string Login { get; set; } = string.Empty;

        // email
        public string Email { get; set; } = string.Empty;

        // api-key в захэшированном виде
        public string ApiKeyHash { get; set; } = string.Empty;

        public User() { }
    }
}
