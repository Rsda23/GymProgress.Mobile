using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymProgress.Mobile.Interfaces;
using GymProgress.Mobile.Services;

namespace GymProgress.Mobile.ViewModels
{
    public partial class SettingViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string titleSetting = "Parametre";

        [ObservableProperty]
        private string pseudo = "Aprilia";
        [ObservableProperty]
        private string email = "aprilia@gmail.com";

        [ObservableProperty]
        private string buttonDisconnectText = "Déconnexion";
        [ObservableProperty]
        private string buttonDeleteAccountText = "Supprimer le compte";



        [RelayCommand]
        private async Task ButtonDisconnect()
        {
            //await Shell.Current.GoToAsync($"/{Routes.LoginPage}");
            //await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            var httpClient = new HttpClient();
            var usersService = new UsersService(httpClient);
            var loginViewModel = new LoginViewModel(usersService);
            var loginPage = new LoginPage(loginViewModel);
            Application.Current.MainPage = new NavigationPage(loginPage);

        }

        [RelayCommand]
        private async Task ButtonDeleteAccount()
        {
            ButtonDisconnect();
        }
    }
}
