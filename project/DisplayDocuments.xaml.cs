using project.data;
using project.model;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace project
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DisplayDocuments : ContentPage
    {
        // Instance of DocumentDatabase
        private DocumentDatabase _documentDatabase;

        // Constructor
        public DisplayDocuments()
        {
            InitializeComponent();

            // Initialize DocumentDatabase (ensure it's correctly initialized)
            _documentDatabase = new DocumentDatabase(); // Change this if you're using dependency injection
        }

        // This method is called when the page appears
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadDocuments();
        }

        // Method to load documents from the database
        private async Task LoadDocuments()
        {
            // Ensure _documentDatabase is not null
            if (_documentDatabase != null)
            {
                var documents = await _documentDatabase.GetDocumentsAsync();
                uploadedDocumentsList.ItemsSource = documents;
            }
            else
            {
                await DisplayAlert("Error", "Document database is not initialized.", "OK");
            }
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
    }
}
