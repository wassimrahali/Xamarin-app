using Project;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace project
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthPage : ContentPage
    {
        public AuthPage()
        {
            InitializeComponent();
        }
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            // Credentials for admin
            string adminEmail = "admin@gmail.com";
            string adminPassword = "admin123";

            // Get user input from Entry fields
            string enteredEmail =UsernameEntry.Text;
            string enteredPassword = PasswordEntry.Text;

            // Validate email and password
            if (string.IsNullOrEmpty(enteredEmail) || string.IsNullOrEmpty(enteredPassword))
            {
                await DisplayAlert("Erreur", "Veuillez remplir tous les champs.", "OK");
                return;
            }

            if (enteredEmail == adminEmail && enteredPassword == adminPassword)
            {
                // Navigate to HomePage if login is successful
                await Navigation.PushAsync(new HomeAdmin());
            }
            else
            {
                // Show error message if login fails
                await DisplayAlert("Échec de connexion", "Email ou mot de passe incorrect. Veuillez réessayer.", "OK");
            }
        }


    }
}