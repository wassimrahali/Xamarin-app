using Project.Data;
using Xamarin.Forms;
using System;
using Project;

namespace project
{
    public partial class LoginStudent : ContentPage
    {
        private StudentDatabase _studentDatabase;

        public LoginStudent()
        {
            InitializeComponent();
            _studentDatabase = App.Database; // Assuming App.DatabasePath is your database path
        }

        private async void OnLoginClickedStudent(object sender, EventArgs e)
        {
            // Get user input from Entry fields
            string enteredCIN = UsernameEntry.Text;  // Assuming UsernameEntry is for CIN
            string enteredPassword = PasswordEntry.Text;

            // Validate CIN and password
            if (string.IsNullOrEmpty(enteredCIN) || string.IsNullOrEmpty(enteredPassword))
            {
                await DisplayAlert("Erreur", "Veuillez remplir tous les champs.", "OK");
                return;
            }

            // Get student data from the database using CIN
            var student = await App.Database.GetStudentAsync(int.Parse(enteredCIN));

            if (student != null && student.Password == enteredPassword)
            {
                // Navigate to the students' page if login is successful
                
                await Navigation.PushAsync(new HomeStudent(student));
            }
            else
            {
                // Show error message if login fails
                await DisplayAlert("Échec de connexion", "CIN ou mot de passe incorrect. Veuillez réessayer.", "OK");
            }
        }
        private async void OnSignUpTapped(object sender, EventArgs e)
        {
            // Navigate to SignUpPage
            await Navigation.PushAsync(new SignupStudent());  // Replace SignUpPage with your actual page
        }
    }
}
