using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymProgress.Domain.Models;
using GymProgress.Mobile.Interfaces;
using System.Collections.ObjectModel;

namespace GymProgress.Mobile.ViewModels
{
    public partial class AddExerciceViewModel : ViewModelBase
    {
        private readonly IExercicesService _exercicesService;
        public AddExerciceViewModel(IExercicesService exercicesService)
        {
            _exercicesService = exercicesService;
            DisplayExercice();
        }

        [ObservableProperty]
        private string buttonCreateExerciceText = "Créer";

        [ObservableProperty]
        private bool isSelected;

        [ObservableProperty]
        private ObservableCollection<Exercice> exercices = new();



        [RelayCommand]
        private async Task ButtonCreateExercice()
        {
            await Shell.Current.GoToAsync("CreateExercicePage");
        }

        [RelayCommand]
        private async Task DisplayExercice()
        {
            var exercices = await _exercicesService.GetAllExercice();
            if (exercices != null)
            {
                foreach (var exercice in exercices)
                {
                    Exercices.Add(exercice);
                }
            }
        }
    }
}
