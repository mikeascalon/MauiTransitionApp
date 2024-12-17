using TransitionApp.ViewModel;

namespace TransitionApp.View;

public partial class TaskPage : ContentPage
{
    private readonly TaskViewModel _taskViewModel;
    public TaskPage(TaskViewModel taskViewModel)
	{
		InitializeComponent();

        _taskViewModel = taskViewModel;

        // Set the BindingContext for data binding
        BindingContext = _taskViewModel;

        // Load tasks when the page appears
        _taskViewModel.LoadTasks();

    }
}