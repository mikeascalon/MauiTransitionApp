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
using TransitionApp.View;



namespace TransitionApp.ViewModel
{
    public class TaskViewModel: INotifyPropertyChanged
    {
        private readonly TaskService _taskService;
        private int _currentUserId; // Pass the UserId dynamically
        private TaskTemplateType _currentTemplateType;


        
        public ICommand DeleteCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand OpenTaskDetailsCommand { get; }
        public ICommand SignOutCommand { get; }


        public ObservableCollection<UserTask> Tasks { get; set; } = new ObservableCollection<UserTask>();
        public ObservableCollection<IGrouping<int, UserTask>> GroupedTasks { get; set; } = new ObservableCollection<IGrouping<int, UserTask>>();

        public TaskViewModel(TaskService taskService)
        {
            _taskService = taskService;

            
            DeleteCommand = new Command<int>(async (taskId) => await DeleteTask(taskId));
            AddCommand = new Command(async () => await OpenAddTaskModal());
            OpenTaskDetailsCommand = new Command<UserTask>(async (task) => await OpenTaskDetailsAsync(task));
            SignOutCommand = new Command(async () => await SignOutAsync());
        }

        public async Task SetUserId(int userId)
        {
            _currentUserId = userId;
            await LoadUserTasksAsync(userId);
        }

        // Load user-specific tasks
        public async Task LoadUserTasksAsync(int userId)
        {
            _currentUserId = userId;

            // Fetch tasks for the current user
            var userTasks = await _taskService.GetTasksByUserAsync(userId);
            Tasks.Clear();
            foreach (var task in userTasks)
            {
                Tasks.Add(task);
            }

            // Group tasks by MonthsLeft for UI grouping
            var grouped = userTasks
                .GroupBy(t => t.MonthsLeft)
                .OrderByDescending(group => group.Key)
                .ToList();

            GroupedTasks.Clear();
            foreach (var group in grouped)
            {
                GroupedTasks.Add(group);
            }

            // Notify the UI about changes
            OnPropertyChanged(nameof(Tasks));
            OnPropertyChanged(nameof(GroupedTasks));

            
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

        private async Task OpenAddTaskModal()
        {
            var addTaskViewModel = new AddTaskViewModel(_taskService, _currentUserId);
            var addTaskPage = new AddTaskPage { BindingContext = addTaskViewModel };

            await Shell.Current.Navigation.PushModalAsync(addTaskPage);

            
        }

        public async Task UpdateTask(UserTask task)
        {
            if (task == null)
            {
                Console.WriteLine("UpdateTask: Task is null");
                return;
            }

            // Debug line
            Console.WriteLine($"UpdateTask: TaskId = {task.TaskId}, TaskName = {task.TaskName}");

            await _taskService.UpdateTaskAsync(task);

            // Reload tasks after updating
            await LoadUserTasksAsync(_currentUserId);
        }

        public async Task DeleteTask(int taskId)
        {
            await _taskService.DeleteTaskAsync(taskId);

            // Reload user tasks after deletion
            await ReloadTasksAsync();
        }

        private async Task OpenTaskDetailsAsync(UserTask task)
        {
            if (task != null)
            {
                await Shell.Current.Navigation.PushModalAsync(new TaskDetailsPage(task, _taskService));
                await ReloadTasksAsync();
            }
        }

        public async Task ReloadTasksAsync()
        {
            var tasks = await _taskService.GetTasksByUserAsync(_currentUserId);
            Tasks.Clear();
            foreach (var task in tasks)
            {
                Tasks.Add(task);
            }

            // Group tasks by MonthsLeft
            var grouped = tasks
                .GroupBy(t => t.MonthsLeft)
                .OrderByDescending(group => group.Key)
                .ToList(); 
            GroupedTasks.Clear();
            foreach (var group in grouped)
            {
                GroupedTasks.Add(group);
            }

            OnPropertyChanged(nameof(Tasks));
            OnPropertyChanged(nameof(GroupedTasks));
        }

        private async Task SignOutAsync()
        {
            // Navigate back to the main page
            await Shell.Current.GoToAsync("//MainPage");
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}

