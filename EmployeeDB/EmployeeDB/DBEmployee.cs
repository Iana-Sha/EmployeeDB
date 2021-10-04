using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace EmployeeDB
{
    public class DBEmployee
    {
        readonly SQLiteAsyncConnection _database;

        public DBEmployee(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Employee>().Wait();
        }
        public Task<List<Employee>> GetPeopleAsync()
        {
            return
            _database.Table<Employee>().ToListAsync();
        }

        public Task<int> SavePersonAsync(Employee
        employee)
        {
            return _database.InsertAsync(employee);
        }

    }
}
