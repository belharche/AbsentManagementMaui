using AbsentManagement.Model;
//using Android.Util;
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
            _connection.ExecuteAsync("PRAGMA foreign_keys = ON;");

            _connection.CreateTableAsync<User>();
            _connection.CreateTableAsync<Student>();
            _connection.CreateTableAsync<Field>();

            //  Default User
            Task.Run(async () =>
            {
                try
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
                }
                catch (Exception ex)
                {
                    
                }
            }).Wait();
        }

        public async Task<List<User>> GetUsers() => await _connection.Table<User>().ToListAsync();

        public async Task<User> GetUserById(int userId)
        {
            return await _connection.Table<User>().Where(user => user.Id == userId).FirstOrDefaultAsync();
        }

        public Task<int> InsertUser(User user) => _connection.InsertAsync(user);

        public Task<int> DeleteUser(User user) => _connection.DeleteAsync(user);


        // Student CRUD operations
        public async Task<List<Student>> GetStudents() => await _connection.Table<Student>().ToListAsync();

        public async Task<Student> GetStudentById(string studentId) =>
            await _connection.Table<Student>().Where(student => student.Id == studentId).FirstOrDefaultAsync();

        public Task<int> InsertStudent(Student student) => _connection.InsertAsync(student);

        public Task<int> UpdateStudent(Student student) => _connection.UpdateAsync(student);

        public Task<int> DeleteStudent(Student student) => _connection.DeleteAsync(student);



        // Field CRUD operations
        public async Task<int> InsertField(Field field)
        {
            return await _connection.InsertAsync(field);
        }

        public async Task<int> DeleteField(Field field)
        {
            // Delete students linked to this field first (because of cascading delete)
            var students = await _connection.Table<Student>().Where(s => s.FieldId == field.Id).ToListAsync();
            foreach (var student in students)
            {
                await _connection.DeleteAsync(student);
            }

            // Now delete the field itself
            return await _connection.DeleteAsync(field);
        }

        public async Task<List<Field>> GetFields() => await _connection.Table<Field>().ToListAsync();

    }
}
