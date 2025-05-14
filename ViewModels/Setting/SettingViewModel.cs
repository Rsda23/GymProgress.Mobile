using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymProgress.Domain.Models;
using GymProgress.Mobile.Services;
using GymProgress.Mobile.ViewModels.SnackBar;

namespace GymProgress.Mobile.ViewModels
{
    public partial class SettingViewModel : ViewModelBase
    {
        private readonly UsersService _usersService;
        private readonly SnackBarViewModel _snackBar;

        public SettingViewModel(UsersService userService, SnackBarViewModel snackBar)
        {
            _usersService = userService;
            _snackBar = snackBar;
        }

        [ObservableProperty]
        private string titleSetting = "Parametre";

        [ObservableProperty]
        private string pseudo = string.Empty;
        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string userId = string.Empty;

        [ObservableProperty]
        private string buttonDisconnectText = "Déconnexion";
        [ObservableProperty]
        private string buttonDeleteAccountText = "Supprimer le compte";

        [ObservableProperty]
        private bool confirm = false;



        [RelayCommand]
        private async Task ButtonDisconnect()
        {
            Preferences.Remove("UserId");
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        [RelayCommand]
        private async Task ButtonDeleteAccount()
        {
            try
            {
                Confirm = await Shell.Current.DisplayAlert(
                "Confirmation",
                "Etes vous sur de vouloir supprimer votre compte ?",
                "Oui", "Non");

                if (Confirm)
                {
                    IsRunning = true;

                    if (string.IsNullOrEmpty(UserId))
                    {
                        throw new Exception("L'id de l'utilisateur est vide");
                    }

                    await _usersService.Delete(UserId);

                    UserId = string.Empty;

                    await ButtonDisconnect();

                    _snackBar.Succefull("Suppression effectuée !");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", ex.Message, "fermer");
            }
            finally
            {
                IsRunning = false;
            }
        }



        public async Task LoadUser()
        {
            IsRunning = true;

            try
            {
                if (Preferences.ContainsKey("UserId"))
                {
                    string userId = Preferences.Get("UserId", string.Empty);
                    if (string.IsNullOrEmpty(userId))
                    {
                        throw new Exception("L'id de l'utilisateur est vide");
                    }

                    User? user = await _usersService.GetUserById(userId);

                    if (user != null)
                    {
                        Pseudo = user.Pseudo;
                        Email = user.Email;
                        UserId = user.UserId;
                    }
                    else
                    {
                        Pseudo = "Erreur";
                        Email = "Erreur";
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", ex.Message, "fermer");
            }
            finally
            {
                IsRunning = false;
            }
        }
    }
}
