using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GymProgress.Mobile.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private string buttonConnectionText = "Se connecter";

        [RelayCommand]
        private async Task ButtonConnection()
        {
            Application.Current.MainPage = new AppShell();
            await Shell.Current.GoToAsync("//SeancePage");
        }
    }
}
