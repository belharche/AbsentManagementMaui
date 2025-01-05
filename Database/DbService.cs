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
            _connection.CreateTableAsync<Lesson>();
            _connection.CreateTableAsync<Absence>();

            // Default User
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

                    await InsertField(new Field(1, "Industriel 1"));
                    await InsertField(new Field(2, "Industriel 2"));
                    await InsertField(new Field(3, "Industriel 3"));
                    await InsertField(new Field(4, "CP 1"));
                    await InsertField(new Field(5, "CP 2"));
                    await InsertField(new Field(6, "Informatique 1"));
                    await InsertField(new Field(7, "Informatique 2"));
                    await InsertField(new Field(8, "Informatique 3"));
                    await InsertField(new Field(9, "AI 1"));
                    await InsertField(new Field(10, "AI 2"));
                    await InsertField(new Field(11, "AI 3"));

                   

                    await InsertStudent(new Student
                    {
                        Id="d1300",
                        Firstname="simo",
                        Lastname= "fadli",
                        Email="salam@gmail.com",
                        Phone="0605543232",
                        FieldId=1


                    });
                   


                }
                catch (Exception ex)
                {
                    // Log exception if necessary
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

        // Lesson CRUD operations
        public async Task<List<Lesson>> GetLessons() => await _connection.Table<Lesson>().ToListAsync();

        public async Task<Lesson> GetLessonById(int lessonId) =>
            await _connection.Table<Lesson>().Where(lesson => lesson.Id == lessonId).FirstOrDefaultAsync();

        public Task<int> InsertLesson(Lesson lesson) => _connection.InsertAsync(lesson);

        public Task<int> UpdateLesson(Lesson lesson) => _connection.UpdateAsync(lesson);

        public Task<int> DeleteLesson(Lesson lesson) => _connection.DeleteAsync(lesson);

        // Absence CRUD operations
        public async Task<List<Absence>> GetAbsences() => await _connection.Table<Absence>().ToListAsync();

        public async Task<Absence> GetAbsenceById(int absenceId) =>
            await _connection.Table<Absence>().Where(absence => absence.AbsenceID == absenceId).FirstOrDefaultAsync();

        public Task<int> InsertAbsence(Absence absence) => _connection.InsertAsync(absence);

        public Task<int> UpdateAbsence(Absence absence) => _connection.UpdateAsync(absence);

        public Task<int> DeleteAbsence(Absence absence) => _connection.DeleteAsync(absence);
        public async Task<List<Absence>> GetAbsenceByUserId(string studentid) =>
            await _connection.Table<Absence>().Where(absence => absence.StudentID == studentid).ToListAsync();
    }
}
