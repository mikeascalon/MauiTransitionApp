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


        // Load tasks based on the selected template
        public void LoadTasks(TaskTemplateType templateType)
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
            LoadTasks(_currentTemplateType);
        }

        public async Task UpdateTask(UserTask task)
        {
            await _taskService.UpdateTaskAsync(task);
            LoadTasks(_currentTemplateType);
        }

        public async Task DeleteTask(int taskId)
        {
            await _taskService.DeleteTaskAsync(taskId);
            LoadTasks(_currentTemplateType); 
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}

