using Microsoft.Maui.Controls;
using TransitionApp.Services;
using TransitionApp.Models;

namespace TransitionApp.View
{
    public partial class MainPage : ContentPage
    {
        private readonly AuthService _authService;

        public MainPage(AuthService authService)
        {
            InitializeComponent();
            // Resolve AuthService from DI container
            _authService = authService;
        }

        private async void OnSignInClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            // Authenticate the user
            var user = _authService.Authenticate(username, password);

            if (user != null && user.UserId > 0) // Ensure userId is valid
            {
                // Hide the keyboard by unfocusing the Entry controls
                UsernameEntry.Unfocus();
                PasswordEntry.Unfocus();

                // Navigate to TaskPage with only userId for existing user tasks
                await Shell.Current.GoToAsync($"TaskPage?userId={user.UserId}");
            }
            else
            {
                ErrorMessage.Text = "Invalid username or password.";
                ErrorMessage.IsVisible = true;
            }
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///SignUpPage");
        }

        
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Clear the Username and Password fields
            UsernameEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;

            // Hide any error messages
            ErrorMessage.IsVisible = false;
        }


    }

}
