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
    public class TaskViewModel
    {
        private readonly TaskService _taskService;
        

        public ObservableCollection<UserTask> Tasks { get; set; } = new ObservableCollection<UserTask>();

        public TaskViewModel(TaskService taskService)
        {
            _taskService = taskService;
        }

        public async void LoadTasks()
        {
            var etsTasks = _taskService.GetTasksForTemplate(TaskTemplateType.ETS);
            Tasks.Clear();
            foreach (var task in etsTasks)
            {
                Tasks.Add(task);
            }
        }

        public async Task AddTask(UserTask task)
        {
            task.UserId = _userId; // Assign current user
            await _taskService.AddTaskAsync(task);
            LoadTasks();
        }

        public async Task UpdateTask(UserTask task)
        {
            await _taskService.UpdateTaskAsync(task);
            LoadTasks();
        }

        public async Task DeleteTask(int taskId)
        {
            await _taskService.DeleteTaskAsync(taskId);
            LoadTasks();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}

