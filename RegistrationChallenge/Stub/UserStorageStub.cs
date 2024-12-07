using RegistrationChallenge.Model.Service;
using RegistrationChallenge.Model.Users;

namespace RegistrationChallenge.Stub
{
    public class UserStorageStub
    {
        private static List<User> users = new List<User>();        

        public async Task InsertAsync(User user)
        {
            await Task.Run(() => users.Add(user));
        }

        public async Task<User?> SelectUserByApiKeyHashAsync(string apiKeyHash)
        {
            return await Task.Run(() => users.FirstOrDefault(u => u.ApiKeyHash == apiKeyHash));
        }

        public async Task<User?> SelectUserByEmailAsync(string email)
        {
            return await Task.Run(() => users.FirstOrDefault(u => u.Email == email));
        }

        public async Task<User?> SelectUserByLoginAsync(string login)
        {
            return await Task.Run(() => users.FirstOrDefault(u => u.Login == login));
        }
    }
}
