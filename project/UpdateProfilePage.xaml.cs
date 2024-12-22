using Project.Models;
using System;
using Xamarin.Forms;

namespace project
{
    public partial class UpdateProfilePage : ContentPage
    {
        private Student _student;

        public UpdateProfilePage(Student student)
        {
            InitializeComponent();

            // Set the current student object and bind to the page's context
            _student = student;

            // Binding the student object to the page's BindingContext
            BindingContext = this;
        }

        // Property to expose the student object for binding
        public Student Student => _student;

        // Event handler for the Save button click
        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            // Validate the input fields before saving
            if (string.IsNullOrWhiteSpace(FirstNameEntry.Text) ||
                string.IsNullOrWhiteSpace(ClassEntry.Text))
            {
                await DisplayAlert("Error", "Please fill in all fields", "OK");
                return;
            }

            // Update the student's properties with the entered data
            _student.FirstName = FirstNameEntry.Text;
            _student.Classe = ClassEntry.Text;

            // Handle the moyenne update if it was entered correctly
            if (double.TryParse(MoyenneEntry.Text, out double moyenne))
            {
                _student.Moyenne = moyenne;
            }
            else
            {
                // If moyenne is invalid, set it to null
                _student.Moyenne = null;
            }

            // For demonstration purposes, here you could save the changes to a database or server

            // Notify user of success and return to the previous page
            await DisplayAlert("Success", "Profile updated successfully", "OK");
            await Navigation.PopAsync(); // Go back to the previous page
        }
    }
}
