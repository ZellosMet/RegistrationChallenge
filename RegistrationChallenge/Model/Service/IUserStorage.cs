using RegistrationChallenge.Model.DataBase;
using RegistrationChallenge.Model.Users;

namespace RegistrationChallenge.Model.Service
{
    // IUserStorage - интерфейс хранилища для работы с пользователями
    public interface IUserStorage
    {
        // SelectUserByLoginAsync - поиск пользователя по логину
        Task<DBUser?> SelectUserByLoginAsync(string login);

        // SelectUserByEmailAsync - поиск пользователя по email
        Task<DBUser?> SelectUserByEmailAsync(string email);

        // SelectUserByApiKeyHashAsync - поиск пользователя по хэшу api-ключа
        Task<DBUser?> SelectUserByApiKeyHashAsync(string apiKeyHash);

        // InsertAsync - добавление пользователя в БД
        Task InsertAsync(DBUser user);
        //ConformEmailAsync - Подтверждение почты пользователя
        Task<DBUser?> ConformEmailAsync(string apiKeyHash);
    }
}
