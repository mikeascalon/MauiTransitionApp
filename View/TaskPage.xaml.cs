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
            _userId = int.Parse(value); // Convert to int
            _taskViewModel.LoadTasks(TaskTemplateType.ETS); // Load tasks for the user
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
                _taskViewModel.LoadTasks(taskTemplateType);
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
}