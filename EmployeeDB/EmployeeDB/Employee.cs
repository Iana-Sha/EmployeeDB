using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace EmployeeDB
{
    public class Employee
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public string Time { get; set; }
        public string Activity { get; set; }
    }
}
