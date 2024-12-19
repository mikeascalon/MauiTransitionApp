using TransitionApp.ViewModel;

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
        var result = await _viewModel.SignUpAsync();

        if (result)
        {
            await DisplayAlert("Success", "Account created successfully!", "OK");
            await Shell.Current.GoToAsync(".."); // Navigate back to MainPage
        }
        else
        {
            ErrorMessage.Text = "Failed to create an account. Please try again.";
            ErrorMessage.IsVisible = true;
        }
    }
}