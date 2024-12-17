using Microsoft.Maui.Controls;
using TransitionApp.Services;

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
            // Retrieve values from Entry fields
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            // Authenticate user
            bool isAuthenticated = _authService.Authenticate(username, password);

            if (isAuthenticated)
            {
                // Navigate to TaskPage after successful login
                await Shell.Current.GoToAsync("//TaskPage");
            }
            else
            {
                // Show error message
                ErrorMessage.Text = "Invalid username or password.";
                ErrorMessage.IsVisible = true;
            }
        }
    }

}
