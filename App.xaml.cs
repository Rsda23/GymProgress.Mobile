using GymProgress.Mobile.Interfaces;
using GymProgress.Mobile.Services;
using GymProgress.Mobile.ViewModels;
using System.Diagnostics;

namespace GymProgress.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            Dispatcher.Dispatch(async () =>
            {
                var httpClient = new HttpClient();
                var usersService = new UsersService(httpClient);
                var loginViewModel = new LoginViewModel(usersService);
                await Shell.Current.Navigation.PushModalAsync(new LoginPage(loginViewModel));
            });
        }
    }
}
