using System;
using System.IO;
using Xamarin.Forms;
using Project.Data;
using project;
using Project.Models;

namespace Project
{
    public partial class App : Application
    {
        static StudentDatabase database;
        public Student _currentStudent;

        // Correct implementation of the currentstudent property
        public Student CurrentStudent
        {
            get => _currentStudent;
            set => _currentStudent = value;
        }


        public static StudentDatabase Database
        {
            get
            {
                if (database == null)
                {
                   
                    database = new StudentDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Students.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new WelcomePage());
        }
    }
}
