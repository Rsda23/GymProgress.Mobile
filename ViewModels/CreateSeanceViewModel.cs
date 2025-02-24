using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GymProgress.Mobile.ViewModels
{
    public partial class CreateSeanceViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string buttonAddExerciceText = "Ajouter";

        [RelayCommand]
        private async Task ButtonAddExercice()
        {
            await Shell.Current.GoToAsync("AddExercicePage");
        }
    }
}
