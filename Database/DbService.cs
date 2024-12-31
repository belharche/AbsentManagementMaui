using AbsentManagement.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AbsentManagement.Database
{
    public class DbService
    {
        private const string DB_NAME = "absent_management_system_db";
        private readonly SQLiteAsyncConnection _connection;

        public DbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<User>();

            //  Default User
            Task.Run(async () =>
            {
                var userCount = await _connection.Table<User>().CountAsync();
                if (userCount == 0)
                {
                    await InsertUser(new User
                    {
                        Email = "admin@email.com",
                        Password = "admin123"
                    });
                }
            }).Wait();
        }

        public async Task<List<User>> GetUsers() => await _connection.Table<User>().ToListAsync();

        public async Task<User> GetUserById(int userId)
        {
            return await _connection.Table<User>().Where(user => user.Id == userId).FirstOrDefaultAsync();
        }

        public Task<int> InserUser(User user) => _connection.InsertAsync(user);

        public Task<int> DeleteUser(User user) => _connection.DeleteAsync(user);

        private async Task InsertUser(User user)
        {
            await _connection.InsertAsync(user);
        }
    }
}
