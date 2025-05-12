using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymProgress.Mobile.Services;

namespace GymProgress.Mobile.ViewModels.Connexion
{
    public partial class ForgotViewModel : ViewModelBase
    {
        private readonly UsersService _usersService;
        public ForgotViewModel(UsersService model)
        {
            _usersService = model;
        }

        [ObservableProperty]
        private bool layoutEmail = true;
        [ObservableProperty]
        private bool layoutCode = false;
        [ObservableProperty]
        private bool layoutPassword = false;



        [RelayCommand]
        private async Task EmailNext()
        {
            LayoutEmail = false;
            LayoutCode = true;
            LayoutPassword = false;
            await Task.Delay(500);
        }

        [RelayCommand]
        private async Task PasswordNext()
        {
            LayoutEmail = false;
            LayoutCode = false;
            LayoutPassword = true;
            await Task.Delay(500);
        }

        [RelayCommand]
        private async Task GoToLogin()
        {
            await Shell.Current.GoToAsync($"/{Routes.LoginPage}");
        }
    }
}
