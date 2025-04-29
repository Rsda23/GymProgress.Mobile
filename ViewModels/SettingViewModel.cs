using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymProgress.Mobile.Interfaces;
using GymProgress.Mobile.Services;

namespace GymProgress.Mobile.ViewModels
{
    public partial class SettingViewModel : ViewModelBase
    {
        private readonly UsersService _usersService;

        public SettingViewModel(UsersService userService)
        {
            _usersService = userService;
        }

        [ObservableProperty]
        private string titleSetting = "Parametre";

        [ObservableProperty]
        private string pseudo = string.Empty;
        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string buttonDisconnectText = "Déconnexion";
        [ObservableProperty]
        private string buttonDeleteAccountText = "Supprimer le compte";



        [RelayCommand]
        private async Task ButtonDisconnect()
        {
            Preferences.Remove("UserId");
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        [RelayCommand]
        private async Task ButtonDeleteAccount()
        {
            ButtonDisconnect();
        }



        public async Task LoadUser()
        {
            if (Preferences.ContainsKey("UserId"))
            {
                string userId = Preferences.Get("UserId", string.Empty);

                var user = await _usersService.GetUserById(userId);

                if (user != null)
                {
                    Pseudo = user.Pseudo;
                    Email = user.Email;
                }
                else
                {
                    Pseudo = "Erreur";
                    Email = "Erreur";
                }
            }
        }
    }
}
