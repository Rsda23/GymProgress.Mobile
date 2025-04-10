using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymProgress.Mobile.Interfaces;
using GymProgress.Mobile.Services;

namespace GymProgress.Mobile.ViewModels
{
    public partial class CreateExerciceViewModel : ViewModelBase
    {
        private readonly IExercicesService _exercicesService;
        public CreateExerciceViewModel(IExercicesService exercicesService)
        {
            _exercicesService = exercicesService;
        }
        [ObservableProperty]
        private string buttonAddExerciceText = "Ajouter";

        [ObservableProperty]
        private string buttonValideText = "Valider";

        [ObservableProperty]
        private string nameExerciceText = string.Empty;



        [RelayCommand]
        private async Task ButtonAddExercice()
        {
            await Shell.Current.GoToAsync("AddExercicePage");
        }

        [RelayCommand]
        private async Task ButtonValide()
        {
            await _exercicesService.PostExercice(NameExerciceText);
            await Shell.Current.GoToAsync($"///{Routes.ExercicePage}");
        }
    }
}
