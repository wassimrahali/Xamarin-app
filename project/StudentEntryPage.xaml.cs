using System;
using Xamarin.Forms;
using Project.Models;

namespace Project
{
    public partial class StudentEntryPage : ContentPage
    {
        public StudentEntryPage()
        {
            InitializeComponent();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var student = (Student)BindingContext;
            await App.Database.SaveStudentAsync(student);
            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var student = (Student)BindingContext;
            await App.Database.DeleteStudentAsync(student);
            await Navigation.PopAsync();
        }
    }
}
