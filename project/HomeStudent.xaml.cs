using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using Project.Models;
using Project.Data;
using project.data;
using project.model;

namespace project
{
    public partial class HomeStudent : ContentPage
    {
        private Student _currentStudent;
        private DocumentDatabase _documentDatabase;

        public HomeStudent(Student student)
        {
            InitializeComponent();

            // Set the current student object
            _currentStudent = student;

            // Initialize the document database
            var dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "documents.db3");
            _documentDatabase = new DocumentDatabase(dbPath);

            // Fetch and display documents
            DisplayDocuments();

            // Update UI with student details
            UpdateUI();
        }

        private void UpdateUI()
        {
            // Set welcome message and class details
            WelcomeLabel.Text = $"Welcome, {_currentStudent.FirstName}";
            ClassLabel.Text = $"Class: {_currentStudent.Classe}";

            // Check if moyenne exists
            if (_currentStudent.Moyenne.HasValue)
            {
                MoyenneLabel.Text = $"Your moyenne: {_currentStudent.Moyenne.Value:F2}";
                MoyenneLabel.TextColor = Color.Green;
            }
            else
            {
                MoyenneLabel.Text = "You don't have any moyenne at the moment. Please check back later.";
                MoyenneLabel.TextColor = Color.Red;
            }
        }

        private async void DisplayDocuments()
        {
            var documents = await _documentDatabase.GetDocumentsAsync();
            uploadedDocumentsList.ItemsSource = documents;
        }

 
        // Event handler for when an item in the ListView is tapped
        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var tappedDocument = e.Item as Document;

            if (tappedDocument != null)
            {
                try
                {
                    // Check if the file exists at the specified path
                    if (System.IO.File.Exists(tappedDocument.FilePath))
                    {
                        // Open the file using the default app (PDF viewer, etc.)
                        await Launcher.OpenAsync(new OpenFileRequest
                        {
                            File = new ReadOnlyFile(tappedDocument.FilePath)
                        });
                    }
                    else
                    {
                        await DisplayAlert("Error", "The document file could not be found.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                }
            }
        }

        // Event handler for the Open button click
        private async void OnOpenButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var tappedDocument = button?.CommandParameter as Document;

            if (tappedDocument != null)
            {
                try
                {
                    // Check if the file exists at the specified path
                    if (System.IO.File.Exists(tappedDocument.FilePath))
                    {
                        // Open the file using the default app (PDF viewer, etc.)
                        await Launcher.OpenAsync(new OpenFileRequest
                        {
                            File = new ReadOnlyFile(tappedDocument.FilePath)
                        });
                    }
                    else
                    {
                        await DisplayAlert("Error", "The document file could not be found.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                }
            }
        }


        // Event handler for the Update Profile button click
        private async void OnUpdateProfileClicked(object sender, EventArgs e)
        {
            // Navigate to the UpdateProfilePage with the current student
            await Navigation.PushAsync(new UpdateProfilePage(_currentStudent));
        }

    }
}
