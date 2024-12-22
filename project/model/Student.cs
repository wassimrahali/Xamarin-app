using System;
using SQLite;

namespace Project.Models
{
    public class Student
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int cin { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Classe { get; set; }
        public double? Moyenne { get; set; }
        public DateTime EnrollmentDate { get; set; }

        [Ignore] 
        public string FullName { get; set; }

        [Ignore] 
        public string MoyenneFormatted { get; set; }
        public string Password { get; set; }  // Added Password field
        public bool IsSuccess { get; internal set; }
    }
}
