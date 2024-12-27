using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TransitionApp.Services;
using TransitionApp.Models;


namespace TransitionApp.ViewModel
{
    public class AddTaskViewModel
    {

        private readonly TaskService _taskService;
        private readonly int _currentUserId;

        public string TaskName { get; set; }
        public int MonthsLeft { get; set; }
        public ICommand SaveCommand { get; }

        public AddTaskViewModel(TaskService taskService, int userId)
        {
            _taskService = taskService;
            _currentUserId = userId;

            SaveCommand = new Command(async () => await SaveTaskAsync());
        }

        private async Task SaveTaskAsync()
        {
            if (string.IsNullOrWhiteSpace(TaskName) || MonthsLeft <= 0)
            {
                // Show validation error if needed
                return;
            }

            var newTask = new UserTask
            {
                TaskName = TaskName,
                MonthsLeft = MonthsLeft,
                IsDone = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserId = _currentUserId
            };

            await _taskService.AddTaskAsync(newTask);

            // Close the modal
            await Shell.Current.Navigation.PopModalAsync();
        }
    }
}
