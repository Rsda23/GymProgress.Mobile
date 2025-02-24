using GymProgress.Mobile.ViewModels;

namespace GymProgress.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            LoginViewModel vm = new LoginViewModel();
            MainPage = new LoginPage(vm);
        }
    }
}
