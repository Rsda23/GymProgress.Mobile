using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GymProgress.Mobile.ViewModels
{
    public partial class ExerciceViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string buttonCreateExerciceText = "Créer";

        [RelayCommand]
        private async Task ButtonCreateExercice()
        {
            await Shell.Current.GoToAsync("CreateExercicePage");
        }
    }
}
