using Project;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace project
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e) { 

                await Navigation.PushAsync(new AuthPage());
            
          
       }

        private async void OnLoginClickedStudent(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new LoginStudent());


        }

    }
}