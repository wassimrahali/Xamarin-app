using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Project.Models;
using Project.Data;
using project.data;
using project.model;

namespace Project
{
    public partial class DocumentPage : ContentPage
    {
        private DocumentDatabase _documentDatabase;
        private FileResult selectedFile;

        public DocumentPage()
        {
            InitializeComponent();
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "documents.db3");
            _documentDatabase = new DocumentDatabase(dbPath); // Initialize the database
        }

        // Pick PDF file
        async void OnPickPdfClicked(object sender, EventArgs e)
        {
            var pickResult = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Pdf,
                PickerTitle = "Pick a PDF document"
            });

            if (pickResult != null)
            {
                selectedFile = pickResult;
                var stream = await selectedFile.OpenReadAsync();
                pdfPreviewImage.Source = ImageSource.FromStream(() => stream); // Display PDF preview
                titleEntry.Text = selectedFile.FileName; // Use file name as default title
            }
        }

        // Submit document with title and description
        async void OnSubmitDocumentClicked(object sender, EventArgs e)
        {
            if (selectedFile != null)
            {
                string title = titleEntry.Text;
                string description = descriptionEditor.Text;

                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description))
                {
                    await DisplayAlert("Error", "Please enter a title and description.", "OK");
                    return;
                }

                var document = new Document
                {
                    Title = title,
                    Description = description,
                    FileName = selectedFile.FileName,
                    FilePath = selectedFile.FullPath // Save the file path locally
                };

                // Save the document to SQLite
                await _documentDatabase.SaveDocumentAsync(document);
                await DisplayAlert("Success", "Document uploaded successfully.", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Please pick a PDF file first.", "OK");
            }
        }

        // Display all uploaded documents
        async void OnDisplayDocumentsClicked(object sender, EventArgs e)
        {
            var documents = await _documentDatabase.GetDocumentsAsync();
            uploadedDocumentsList.ItemsSource = documents;
        }

        // Handle document tap to open it
        private async void OnDocumentTapped(object sender, ItemTappedEventArgs e)
        {
            var tappedDocument = e.Item as Document;

            if (tappedDocument != null)
            {
                try
                {
                    // Check if the file exists at the specified path
                    if (File.Exists(tappedDocument.FilePath))
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

        // Delete the document from the database and update the list
        async void OnDeleteDocumentClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var documentToDelete = button?.CommandParameter as Document;

            if (documentToDelete != null)
            {
                bool confirmDelete = await DisplayAlert("Confirm Deletion", "Are you sure you want to delete this document?", "Yes", "No");

                if (confirmDelete)
                {
                    // Delete the document from the database
                    await _documentDatabase.DeleteDocumentAsync(documentToDelete);

                    // Refresh the document list
                    var documents = await _documentDatabase.GetDocumentsAsync();
                    uploadedDocumentsList.ItemsSource = documents;
                }
            }
        }
    }
}
