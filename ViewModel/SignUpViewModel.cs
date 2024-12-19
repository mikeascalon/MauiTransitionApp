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
    public class SignUpViewModel: INotifyPropertyChanged
    {

        private readonly AuthService _authService; // Service to handle authentication and user creation
        private readonly TaskService _taskService; // Service to handle tasks

        private TaskTemplateType _selectedTemplate; // Backing field for selected template

        public SignUpViewModel(AuthService authService, TaskService taskService)
        {
            _authService = authService;
            _taskService = taskService;

            // Initialize TemplateOptions
            TemplateOptions = Enum.GetValues(typeof(TaskTemplateType)).Cast<TaskTemplateType>().ToList();
        }

        // Property for template options (for the Picker ItemsSource)
        public List<TaskTemplateType> TemplateOptions { get; }

        // Property for the selected template
        public TaskTemplateType SelectedTemplate
        {
            get => _selectedTemplate;
            set
            {
                _selectedTemplate = value;
                OnPropertyChanged();
            }
        }


        public async Task<bool> SignUpUserAsync(string username, string password, string email, TaskTemplateType selectedTemplate)
        {
            // Create the new user
            var newUser = new User
            {
                Username = username,
                PasswordHash = password, // Store hashed password in production
                Email = email,
                TaskTemplate = selectedTemplate,
                CreatedAt = DateTime.UtcNow
            };

            // Add the user to the database
            var userAdded = await _authService.RegisterUserAsync(newUser);

            if (!userAdded) return false;

            // Retrieve tasks from the selected template
            var templateTasks = await _taskService.GetTasksForTemplateAsync(selectedTemplate);

            // Add template tasks to the user's task list
            foreach (var templateTask in templateTasks)
            {
                var userTask = new UserTask
                {
                    TaskName = templateTask.Name,
                    MonthsLeft = templateTask.MonthsLeft ?? 0,
                    IsDone = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = newUser.UserId
                };

                await _taskService.AddTaskAsync(userTask);
            }

            return true;
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
