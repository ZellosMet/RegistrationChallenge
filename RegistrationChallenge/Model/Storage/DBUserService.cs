using Microsoft.EntityFrameworkCore;
using RegistrationChallenge.Model.Service;
using RegistrationChallenge.Model.Users;
using System.Diagnostics.Metrics;

namespace RegistrationChallenge.Model.DataBase
{
    public class DBUserService : IUserStorage
    {
        private readonly UsersDBContext context;
        public DBUserService(UsersDBContext usersDBContext)
        {
            context = usersDBContext;
        }

        // SelectUserByLoginAsync - поиск пользователя по логину
        public async Task<DBUser?> SelectUserByLoginAsync(string login)
        {
            DBUser user = await context.Users.FirstOrDefaultAsync(u => u.Login == login);
            return user;
        }

        // SelectUserByEmailAsync - поиск пользователя по email
        public async Task<DBUser?> SelectUserByEmailAsync(string email)
        {
            DBUser user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        // SelectUserByApiKeyHashAsync - поиск пользователя по хэшу api-ключа
        public async Task<DBUser?> SelectUserByApiKeyHashAsync(string apiKeyHash)
        {
            DBUser user = await context.Users.FirstOrDefaultAsync(u => u.ApiKeyHash == apiKeyHash);
            return user;
        }

        // InsertAsync - добавление пользователя в БД
        public async Task InsertAsync(DBUser user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();        
        }
        public async Task<DBUser?> ConformEmailAsync(string apiKeyHash)
        {
            DBUser user = await context.Users.FirstOrDefaultAsync(u => u.ApiKeyHash == apiKeyHash);
            user.ConfirmEmail = true;
            await context.SaveChangesAsync();
            return user;
        }
    }
}
