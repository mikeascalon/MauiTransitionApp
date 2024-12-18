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
                await Shell.Current.GoToAsync($"TaskPage?userId={user.UserId}&templateType={user.TaskTemplate}");
            }
            else
            {
                ErrorMessage.Text = "Invalid username or password.";
                ErrorMessage.IsVisible = true;
            }
        }
    }

}
