using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymProgress.Mobile.Interfaces;
using GymProgress.Mobile.Services;

namespace GymProgress.Mobile.ViewModels
{
    public partial class SettingViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string buttonDisconnectText = "Déconnexion";

        [ObservableProperty]
        private string pseudo = "Aprilia";

        [ObservableProperty]
        private string email = "aprilia@gmail.com";

        [ObservableProperty]
        private string buttonDeleteAccountText = "Supprimer le compte";

        [ObservableProperty]
        private string titleSetting = "Parametre";

        [RelayCommand]
        private async Task ButtonDisconnect()
        {
            IUsersService us = new UsersService(new HttpClient());
            Application.Current.MainPage = new LoginPage(new LoginViewModel(), us);
        }

        [RelayCommand]
        private async Task ButtonDeleteAccount()
        {
            //Suppression
            ButtonDisconnect();
        }
    }
}
