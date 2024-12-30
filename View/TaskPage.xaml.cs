using TransitionApp.ViewModel;
using TransitionApp.Models;

namespace TransitionApp.View;

[QueryProperty(nameof(UserId), "userId")]
[QueryProperty(nameof(TemplateType), "templateType")]
public partial class TaskPage : ContentPage
{
    private readonly TaskViewModel _taskViewModel;

    private int _userId;
    private string _templateType;

    public string UserId
    {
        set
        {
            if (int.TryParse(value, out int parsedUserId))
            {
                _ = _taskViewModel.LoadUserTasksAsync(parsedUserId); // Load user's tasks
            }
        }
    }


    public string TemplateType
    {
        set
        {
            _templateType = value;

            // Convert templateType to enum and load tasks
            if (Enum.TryParse<TaskTemplateType>(_templateType, out var taskTemplateType))
            {
                _taskViewModel.LoadTemplateTasks(taskTemplateType);
            }
            else
            {
                // Handle invalid template type
                _taskViewModel.LoadTemplateTasks(TaskTemplateType.ETS); // Default template
            }
        }
    }
    public TaskPage(TaskViewModel taskViewModel)
	{
		InitializeComponent();

        _taskViewModel = taskViewModel;

        // Set the BindingContext for data binding
        BindingContext = _taskViewModel;

        

    }

    public async Task SetParametersAsync(int userId, TaskTemplateType templateType)
    {
        if (userId > 0)
        {
            // Load user-specific tasks asynchronously
            await _taskViewModel.LoadUserTasksAsync(userId);
        }
        else
        {
            // Load tasks from the selected template
            _taskViewModel.LoadTemplateTasks(templateType);
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (_taskViewModel != null)
        {
            // Reload the tasks
            _ = _taskViewModel.ReloadTasksAsync();
        }
    }

   
}