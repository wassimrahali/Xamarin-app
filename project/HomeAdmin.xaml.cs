using Project;
using System;
using Xamarin.Forms;

namespace project
{
    public partial class HomeAdmin : ContentPage
    {
        public HomeAdmin()
        {
            InitializeComponent();
        }

        // Handle rectangle clicks
        private async void OnRectangleClicked(object sender, EventArgs e)
        {
            // Get the sender, which is the Frame that was tapped
            var tappedFrame = (Frame)sender;

            // Retrieve the CommandParameter from the GestureRecognizer
            var parameter = ((TapGestureRecognizer)tappedFrame.GestureRecognizers[0]).CommandParameter.ToString();

            // Navigate based on the CommandParameter
            switch (parameter)
            {
                case "Add Moyenne":
                    await Navigation.PushAsync(new StudentsPage());
                    break;
                case "Add Document":
                    await Navigation.PushAsync(new DocumentPage());
                    break;
                case "Display Documents":
                    await Navigation.PushAsync(new DisplayDocuments());
                    break;
                case "More":
                    await Navigation.PushAsync(new DocumentPage());
                    break;
                default:
                    break;
            }
        }
    }
}
