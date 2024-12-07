using RegistrationChallenge.Model.DataBase;
using RegistrationChallenge.Model.Exceptions;
using RegistrationChallenge.Model.Service;
using System.Text.RegularExpressions;

namespace RegistrationChallenge.Model.Users
{
    // UserService - класс с операциями для работы с пользователями
    public class UserService
    {
        // испольуемые сервисы
        private readonly IUserStorage _storage;
        private readonly IEmailService _email;
        private readonly IEncoder _apiKeyGenerator; // encoder для создания api-ключа
        private readonly IEncoder _apiKeyEncoder;   // encoder для шифрования api-ключа при сохранении в сторадж

        public UserService(IUserStorage storage, 
            IEmailService email, 
            [FromKeyedServices("api-key-generator")] IEncoder apiKeyGenerator, 
            [FromKeyedServices("api-key-encoder")] IEncoder apiKeyEncoder) 
        {
            _storage = storage;
            _email = email;
            _apiKeyGenerator = apiKeyGenerator;
            _apiKeyEncoder = apiKeyEncoder;
        }

        // RegisterAsync - метод регистрации пользователя с отправкой API-ключа по почте
        // вход: логин и email пользователя
        // выход: -
        // исключения: UserFormatException, DuplicatedDataException
        public async Task RegisterAsync(string login, string email)
        {
            // 1. валидация данных
            ValidateLogin(login);
            ValidateEmail(email);

            // 2. проверить, нет ли пользователя с таким логином или email-ом
            if (await _storage.SelectUserByLoginAsync(login) != null)
            {
                throw new DuplicatedDataException("login", login);
            }
            if (await _storage.SelectUserByEmailAsync(email) != null)
            {
                throw new DuplicatedDataException("email", email);
            }

            // 3. зарегистрировать пользователя
            //User user = new User()
            //{
            //    UUID = Guid.NewGuid(),
            //    Login = login,
            //    Email = email
            //};
            //string seed = $"{user.UUID} - {user.Login} - {user.Email}";
            //string userApiKey = _apiKeyGenerator.Encode(seed);      // сгенерировали api-key
            //user.ApiKeyHash = _apiKeyEncoder.Encode(userApiKey);    // захэшированный api-key в базу

            DBUser user = new DBUser()
            {
                UUID = Guid.NewGuid(),
                Login = login,
                Email = email
            };
            string seed = $"{user.UUID} - {user.Login} - {user.Email}";
            string userApiKey = _apiKeyGenerator.Encode(seed);      // сгенерировали api-key
            user.ApiKeyHash = _apiKeyEncoder.Encode(userApiKey);    // захэшированный api-key в базу

            // 4. отправить ключ на почту
            // await _email.SendMailAsync(email, "Registration", $"Your API key: {userApiKey}");
            await _email.SendMailAsync(email, "Registration", $"Click on the link to confirm your email: http://localhost:8080/api/user/{user.ApiKeyHash}");

            // 5. добавить пользователя в БД
            await _storage.InsertAsync(user);
        }

        // GetAsync - метод получения информации о пользователе по api-ключу
        // вход: строку api-ключа
        // выход: объект с информацией о пользователе
        // исключения: UserNotFoundException
        public async Task<DBUser> GetAsync(string apiKey)
        {
            //string apiKeyHash = _apiKeyEncoder.Encode(apiKey);
            //DBUser? user = await _storage.SelectUserByApiKeyHashAsync(apiKeyHash);
            DBUser? user = await _storage.SelectUserByApiKeyHashAsync(apiKey);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            return user;
        }
        public async Task<DBUser> ConfirmEmailAsync(string apiKey)
        {
           
            DBUser? user = await _storage.ConformEmailAsync(apiKey);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            return user;
        }

        // вспомогательные методы валидации
        private static readonly Regex validLogin = new Regex("^(?![_ -])(?:(?![_ -]{2})[\\w -]){5,16}(?<![_ -])$");

        private void ValidateLogin(string login)
        {
            if (string.IsNullOrEmpty(login))
            {
                throw new UserFormatException($"login is invalid: null or empty string");
            }
            if (!validLogin.IsMatch(login))
            {
                throw new UserFormatException($"login is invalid: login contains invalid symbols");
            }
        }

        private static readonly Regex validEmail = new Regex("^([a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\\.[a-zA-Z0-9_-]+)$");

        private void ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new UserFormatException($"email is invalid: null or empty string");
            }
            if (!validEmail.IsMatch(email))
            {
                throw new UserFormatException($"email is invalid: email contains invalid symbols");
            }
        }
    }
}
