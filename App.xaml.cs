using GymProgress.Mobile.Interfaces;
using GymProgress.Mobile.Services;
using GymProgress.Mobile.ViewModels;
using System.Diagnostics;

namespace GymProgress.Mobile
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            //_serviceProvider = serviceProvider;

            //MainPage = _serviceProvider.GetRequiredService<LoginPage>();

            //HttpClient httpClient = new HttpClient();
            //IUsersService us = new UsersService(httpClient);

            //var userService = DependencyService.Resolve<UsersService>();
            //MainPage = new LoginPage(new LoginViewModel(userService));


            MainPage = new AppShell();

            //var httpClient = new HttpClient();
            //var usersService = new UsersService(httpClient);
            //var usersService = DependencyService.Resolve<UsersService>();

            //var loginViewModel = new LoginViewModel(usersService);

            //MainPage = new LoginPage(loginViewModel);
        }
    }
}
