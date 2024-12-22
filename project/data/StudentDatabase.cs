using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Project.Models;

namespace Project.Data
{
    public class StudentDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public StudentDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Student>().Wait();
        }

        public Task<List<Student>> GetStudentsAsync()
        {
            return _database.Table<Student>().ToListAsync();
        }

        public Task<Student> GetStudentAsync(int cin)
        {
            return _database.Table<Student>()
                            .Where(i => i.cin == cin)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveStudentAsync(Student student)
        {
            if (student.ID != 0)
            {
                return _database.UpdateAsync(student);
            }
            else
            {
                return _database.InsertAsync(student);
            }
        }

        public Task<int> DeleteStudentAsync(Student student)
        {
            return _database.DeleteAsync(student);
        }
    }
}
