using Project;
using Project.Data;
using Project.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace project
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupStudent : ContentPage
    {
        private StudentDatabase _studentDatabase;

        public SignupStudent()
        {
            InitializeComponent();

            // Populate the class picker with predefined options
            ClassePicker.ItemsSource = new System.Collections.Generic.List<string>
            {
                "DSI21", "DSI22", "DSI23", "DSI32", "DSI33", "DSI31", "TI11", "TI12", "TI13"
            };
        }

        private async void OnSignupButtonClicked(object sender, EventArgs e)
        {
            // Retrieve the values from the Entry fields
            string fullName = FullNameEntry.Text;
            string cin = CINEntry.Text;
            string password = PasswordEntry.Text;
            string selectedClass = ClassePicker.SelectedItem?.ToString(); // Get the selected class

            // Validate the inputs
            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(cin) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(selectedClass))
            {
                await DisplayAlert("Validation Error", "Please fill in all fields", "OK");
                return;
            }

            // Create a new student object
            var newStudent = new Student
            {
                FullName = fullName,
                cin = int.Parse(cin),  // Assuming CIN is an integer
                FirstName = fullName.Split(' ')[0], // Extracting first name from full name
                LastName = fullName.Split(' ').Length > 1 ? fullName.Split(' ')[1] : "", // Extracting last name
                Password = password,  // Store password (you may want to hash it before saving)
                Classe = selectedClass,
                EnrollmentDate = DateTime.Now
            };

            // Save the student to the database
            var result = await App.Database.SaveStudentAsync(newStudent);

            if (result > 0)
            {
                await DisplayAlert("Success", "Student signed up successfully!", "OK");
                // Navigate back to the previous page (or another page)
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "There was an issue signing up the student. Please try again.", "OK");
            }
        }
    }
}
