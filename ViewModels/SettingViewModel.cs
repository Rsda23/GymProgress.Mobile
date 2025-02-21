using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GymProgress.Mobile.ViewModels
{
    public partial class SettingViewModel : ObservableObject
    {
        [ObservableProperty]
        private string buttonDisconnectText = "Déconnexion";

        [RelayCommand]
        private async Task ButtonDisconnect()
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
