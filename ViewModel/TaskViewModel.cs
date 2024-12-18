using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TransitionApp.Models;
using TransitionApp.Services;


namespace TransitionApp.ViewModel
{
    public class TaskViewModel: INotifyPropertyChanged
    {
        private readonly TaskService _taskService;
        private int _currentUserId; // Pass the UserId dynamically
        private TaskTemplateType _currentTemplateType;
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        public ObservableCollection<UserTask> Tasks { get; set; } = new ObservableCollection<UserTask>();

        public TaskViewModel(TaskService taskService)
        {
            _taskService = taskService;

            UpdateCommand = new Command<UserTask>(async (task) => await UpdateTask(task));
            DeleteCommand = new Command<int>(async (taskId) => await DeleteTask(taskId));

        }

        public void SetUserId(int userId)
        {
            _currentUserId = userId;
            LoadTasks(TaskTemplateType.ETS); // Default to ETS
        }

        // Load user-specific tasks
        public async Task LoadUserTasksAsync(int userId)
        {
            _currentUserId = userId;

            var userTasks = await _taskService.GetTasksByUserAsync(userId);
            Tasks.Clear();

            foreach (var task in userTasks)
            {
                Tasks.Add(task);
            }

            OnPropertyChanged(nameof(Tasks));
        }


        // Load tasks from a template
        public void LoadTemplateTasks(TaskTemplateType templateType)
        {
            _currentTemplateType = templateType;

            var templateTasks = _taskService.GetTasksForTemplate(templateType);
            Tasks.Clear();

            foreach (var task in templateTasks)
            {
                Tasks.Add(task);
            }

            OnPropertyChanged(nameof(Tasks));
        }

        public async Task AddTask(UserTask task)
        {
            task.UserId = _currentUserId; // Assign current user
            await _taskService.AddTaskAsync(task);

            // Reload user tasks after adding
            await LoadUserTasksAsync(_currentUserId);
        }

        public async Task UpdateTask(UserTask task)
        {
            await _taskService.UpdateTaskAsync(task);

            // Reload user tasks after updating
            await LoadUserTasksAsync(_currentUserId);
        }

        public async Task DeleteTask(int taskId)
        {
            await _taskService.DeleteTaskAsync(taskId);

            // Reload user tasks after deletion
            await LoadUserTasksAsync(_currentUserId);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}

