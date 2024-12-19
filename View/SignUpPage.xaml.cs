using TransitionApp.ViewModel;
using TransitionApp.Models;

namespace TransitionApp.View;

public partial class SignUpPage : ContentPage
{
    private readonly SignUpViewModel _viewModel;
    public SignUpPage(SignUpViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private async void OnSignUpClicked(object sender, EventArgs e)
    {
        // Validate user input
        if (string.IsNullOrWhiteSpace(UsernameEntry.Text) ||
            string.IsNullOrWhiteSpace(PasswordEntry.Text) ||
            string.IsNullOrWhiteSpace(EmailEntry.Text) ||
            TemplatePicker.SelectedItem == null)
        {
            ErrorMessage.Text = "Please fill out all fields.";
            ErrorMessage.IsVisible = true;
            return;
        }

        // Gather user input
        string username = UsernameEntry.Text.Trim();
        string password = PasswordEntry.Text.Trim();
        string email = EmailEntry.Text.Trim();
        TaskTemplateType selectedTemplate = (TaskTemplateType)TemplatePicker.SelectedItem;

        // Call ViewModel to handle sign-up logic
        var result = await _viewModel.SignUpUserAsync(username, password, email, selectedTemplate);

        if (result)
        {
            await DisplayAlert("Success", "Account created successfully!", "OK");
            await Shell.Current.GoToAsync("///MainPage"); // Ensure proper navigation to MainPage
        }
        else
        {
            ErrorMessage.Text = "Failed to create account. Please try again.";
            ErrorMessage.IsVisible = true;
        }
    }

}