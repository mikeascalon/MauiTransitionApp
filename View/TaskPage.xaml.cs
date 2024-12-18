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
            if (int.TryParse(value, out int userId))
            {
                _userId = userId;
                _taskViewModel.SetUserId(userId);
            }
            else
            {
                Console.WriteLine("Invalid userId passed to TaskPage.");
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

    public void SetParameters(int userId, TaskTemplateType templateType)
    {
        _taskViewModel.SetUserId(userId);
        _taskViewModel.LoadTasks(templateType);
    }
}