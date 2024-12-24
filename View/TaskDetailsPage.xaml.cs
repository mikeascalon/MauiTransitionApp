using TransitionApp.Models;
using TransitionApp.Services;
using TransitionApp.ViewModel;

namespace TransitionApp.View;

public partial class TaskDetailsPage : ContentPage
{
	

    public TaskDetailsPage(UserTask task, TaskService taskService)
    {
        InitializeComponent();
        BindingContext = new TaskDetailsViewModel(task, taskService);
    }
}