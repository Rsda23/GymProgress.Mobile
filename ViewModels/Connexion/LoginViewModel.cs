using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymProgress.Domain.Models;
using GymProgress.Mobile.Services;

namespace GymProgress.Mobile.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        private readonly UsersService _usersService;
        public LoginViewModel(UsersService usersService)
        {
            _usersService = usersService;
        }

        [ObservableProperty]
        private string email = string.Empty;
        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private string errorText = "Email ou mot de passe incorrect.";

        [ObservableProperty]
        private bool error = false;

        [ObservableProperty]
        private string buttonConnectionText = "Se connecter";

        [ObservableProperty]
        private bool eyeYes = true;
        [ObservableProperty]
        private bool eyeNo = false;

        [ObservableProperty]
        private bool isPasswordVisible;
        [ObservableProperty]
        private bool isPasswordField = true;



        [RelayCommand]
        private async void GoToSeance()
        {
            await Shell.Current.GoToAsync($"/{Routes.SeancePage}");
        }

        [RelayCommand]
        private async void GoToSubscribe()
        {
            await Shell.Current.GoToAsync($"/{Routes.SubscribePage}");
        }

        [RelayCommand]
        private async void GoToForgot()
        {
            await Shell.Current.GoToAsync($"/{Routes.ForgotPage}");
        }

        [RelayCommand]
        private async Task ButtonConnection()
        {
            try
            {
                User user = await _usersService.GetUserByEmail(Email);

                if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
                {
                    throw new Exception("Tous les champs doivent être remplis.");
                }

                if (user == null)
                {
                    Error = true;
                    return;
                }

                bool password = PasswordHashing.ConfirmPassword(Password, user.HashedPassword);

                if (!password)
                {
                    Error = true;
                    return;
                }

                GoToSeance();
            }
            catch (Exception ex)
            {
                ErrorText = ex.Message;
                Error = true;
            }
        }

        [RelayCommand]
        private void PasswordVisibility()
        {
            IsPasswordVisible = !IsPasswordVisible;
            IsPasswordField = !IsPasswordVisible;

            if (IsPasswordVisible)
            {
                EyeYes = false;
                EyeNo = true;
            }
            else
            {
                EyeYes = true;
                EyeNo = false;
            }
        }
    }
}
