using System;
using Xamarin.Forms;
using Project.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project;

namespace project
{
    public partial class StudentsPage : ContentPage
    {
        private List<Student> allStudents = new List<Student>();

        public StudentsPage()
        {
            InitializeComponent();
        }

        private void PopulateStudentGrid(List<Student> students)
        {
            StudentsGrid.Children.Clear();

            Color rowBackgroundColor = Color.FromHex("#f0f8ff"); // Light background for rows
            Color headerBackgroundColor = Color.FromHex("#4caf50"); // Green shade for header
            Color headerTextColor = Color.White; // Keep header text white for contrast
            Color rowBackgroundColor1 = Color.FromHex("#f9fbe7"); // Light green tint
            Color rowBackgroundColor2 = Color.FromHex("#e8f5e9"); // Slightly darker green tint
            Color borderColor = Color.FromHex("#388e3c"); // Dark green for borders


            // Add headers
            StudentsGrid.Children.Add(CreateCell("Cin", headerBackgroundColor, headerTextColor, borderColor, true), 0, 0);
            StudentsGrid.Children.Add(CreateCell("Name", headerBackgroundColor, headerTextColor, borderColor, true), 1, 0);
            StudentsGrid.Children.Add(CreateCell("Class", headerBackgroundColor, headerTextColor, borderColor, true), 2, 0);
            StudentsGrid.Children.Add(CreateCell("Actions", headerBackgroundColor, headerTextColor, borderColor, true), 3, 0);

            // Add students
            for (int i = 0; i < students.Count; i++)
            {
                var student = students[i];
                int rowIndex = i + 1;

                // Add CIN
                StudentsGrid.Children.Add(CreateCell(student.cin.ToString(), rowBackgroundColor, Color.Black, borderColor), 0, rowIndex);

                // Add Name
                StudentsGrid.Children.Add(CreateCell(student.FullName, rowBackgroundColor, Color.Black, borderColor), 1, rowIndex);

                // Add Class
                StudentsGrid.Children.Add(CreateCell(student.Classe, rowBackgroundColor, Color.Black, borderColor), 2, rowIndex);

                // Add action buttons
                var actionLayout = new StackLayout { Orientation = StackOrientation.Horizontal, Spacing = 5 };

                var updateButton = new ImageButton
                {
                    Source = "https://cdn-icons-png.flaticon.com/128/8771/8771493.png",
                    BackgroundColor = Color.Transparent,
                    HeightRequest = 30,
                    WidthRequest = 30
                };
                updateButton.Clicked += (s, e) => OnUpdateClicked(student);

                var deleteButton = new ImageButton
                {
                    Source = "https://cdn-icons-png.flaticon.com/128/6861/6861362.png",
                    BackgroundColor = Color.Transparent,
                    HeightRequest = 30,
                    WidthRequest = 30
                };
                deleteButton.Clicked += (s, e) => OnDeleteClicked(student);

                actionLayout.Children.Add(updateButton);
                actionLayout.Children.Add(deleteButton);

                var actionCell = new Frame
                {
                    Content = actionLayout,
                    BorderColor = borderColor,
                    BackgroundColor = rowBackgroundColor,
                    Padding = 5,
                    Margin = 1
                };

                StudentsGrid.Children.Add(actionCell, 3, rowIndex);
            }
        }

        private Frame CreateCell(string text, Color backgroundColor, Color textColor, Color borderColor, bool isHeader = false)
        {
            return new Frame
            {
                Content = new Label
                {
                    Text = text,
                    TextColor = textColor,
                    FontAttributes = isHeader ? FontAttributes.Bold : FontAttributes.None,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center
                },
                BorderColor = borderColor,
                BackgroundColor = backgroundColor,
                Padding = 5,
                Margin = 1
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Populate ClassPicker
            ClassPicker.ItemsSource = new List<string>
            {
                "All Classes", "DSI21", "DSI22", "DSI23", "DSI32", "DSI33", "DSI31", "TI11", "TI12", "TI13"
            };

            await LoadStudents();
        }

        private async Task LoadStudents()
        {
            var students = await App.Database.GetStudentsAsync();

            foreach (var student in students)
            {
                student.FullName = $"{student.FirstName} {student.LastName}";
            }

            allStudents = students;
            PopulateStudentGrid(allStudents);
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue?.ToLower() ?? "";

            var filteredStudents = allStudents
                .Where(s => s.FullName.ToLower().Contains(searchText) || s.Classe.ToLower().Contains(searchText))
                .ToList();

            PopulateStudentGrid(filteredStudents);
        }

        private void OnClassFilterChanged(object sender, EventArgs e)
        {
            string selectedClass = ClassPicker.SelectedItem?.ToString();

            if (selectedClass == "All Classes" || string.IsNullOrEmpty(selectedClass))
            {
                PopulateStudentGrid(allStudents);
            }
            else
            {
                var filteredStudents = allStudents
                    .Where(s => s.Classe == selectedClass)
                    .ToList();

                PopulateStudentGrid(filteredStudents);
            }
        }

        private async void OnStudentAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StudentEntryPage
            {
                BindingContext = new Student()
            });
        }

        private async void OnUpdateClicked(Student student)
        {
            await Navigation.PushAsync(new StudentEntryPage
            {
                BindingContext = student
            });
        }

        private async void OnDeleteClicked(Student student)
        {
            bool confirm = await DisplayAlert("Confirm", $"Are you sure you want to delete {student.FullName}?", "Yes", "No");

            if (confirm)
            {
                await App.Database.DeleteStudentAsync(student);
                allStudents.Remove(student);
                PopulateStudentGrid(allStudents);
            }
        }

        private async void addDocument(object sender, EventArgs e)
        {
            // Navigate to DocumentPage
            await Navigation.PushAsync(new DocumentPage());
        }
    }
}
