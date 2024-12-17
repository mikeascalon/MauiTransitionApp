using TransitionApp.View;

namespace TransitionApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("TaskPage", typeof(TaskPage));


        }
    }
}
