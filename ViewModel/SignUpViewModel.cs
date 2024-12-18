using System;
using System.Collections.Generic;
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
        private string _username;
        private string _password;
        private string _email;
        private TaskTemplateType _selectedTemplate;

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
            set { _selectedTemplate = value; OnPropertyChanged(); }
        }

        public SignUpViewModel(AuthService authService)
        {
            _authService = authService;
        }

        public async Task<bool> SignUpAsync()
        {
            var newUser = new User
            {
                Username = Username,
                PasswordHash = Password, // In real-world apps, hash the password!
                Email = Email,
                TaskTemplate = SelectedTemplate
            };

            return await _authService.RegisterUserAsync(newUser);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
