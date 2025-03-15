using GymProgress.Mobile.Interfaces;
using GymProgress.Mobile.Services;
using GymProgress.Mobile.ViewModels;

namespace GymProgress.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //LoginViewModel vm = new LoginViewModel();
            //HttpClient httpClient = new HttpClient();
            //IUsersService us = new UsersService(httpClient);
            // MainPage = new LoginPage(vm, us);

            MainPage = new AppShell();
        }
    }
}
