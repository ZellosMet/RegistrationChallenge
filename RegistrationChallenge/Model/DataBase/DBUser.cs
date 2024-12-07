using System.ComponentModel.DataAnnotations;

namespace RegistrationChallenge.Model.DataBase
{
    public class DBUser
    {
        // uuid пользователя
        [Key]
        public Guid UUID { get; set; }

        // логин пользователя
        public string Login { get; set; } = string.Empty;

        // email
        public string Email { get; set; } = string.Empty;

        // api-key в захэшированном виде
        public string ApiKeyHash { get; set; } = string.Empty;
        public bool ConfirmEmail { get; set; } = false;

        public DBUser() { }
    }
}
