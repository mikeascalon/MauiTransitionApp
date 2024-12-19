using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TransitionApp.Models;
using TransitionApp.Services;

namespace TransitionApp.ViewModel
{
    public class SignUpViewModel:INotifyPropertyChanged
    {

        private readonly AuthService _authService;

        // User input properties
        private string _username;
        private string _password;
        private string _email;
        private TaskTemplateType _selectedTemplate;

        // Expose available templates to bind to Picker
        public ObservableCollection<TaskTemplateType> Templates { get; }


     
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public TaskTemplateType SelectedTemplate
        {
            get => _selectedTemplate;
            set
            {
                _selectedTemplate = value;
                OnPropertyChanged();
            }
        }


        public SignUpViewModel(AuthService authService)
        {
            _authService = authService;

            // Load enum values into the ObservableCollection
            Templates = new ObservableCollection<TaskTemplateType>(
                Enum.GetValues(typeof(TaskTemplateType)).Cast<TaskTemplateType>()
            );

        }

        // Command to handle sign-up logic

        public async Task<bool> SignUpAsync()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                return false; // Validation failed
            }

            // Create a new user object
            var newUser = new User
            {
                Username = Username,
                PasswordHash = Password, // Ideally, hash the password
                Email = Email,
                TaskTemplate = SelectedTemplate
            };

            // Use AuthService to save the user
            return await _authService.RegisterUserAsync(newUser);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
