using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GymProgress.Mobile.ViewModels
{
    public partial class LoginViewModel : ObservableObject
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
            await Shell.Current.GoToAsync("//SeancePage");
        }
    }
}
