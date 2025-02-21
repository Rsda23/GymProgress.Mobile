namespace GymProgress.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CreateSeancePage), typeof(CreateSeancePage));
            Routing.RegisterRoute(nameof(AddExercicePage), typeof(AddExercicePage));
            Routing.RegisterRoute(nameof(CreateExercicePage), typeof(CreateExercicePage));
        }
    }
}
