using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymProgress.Domain.Models;
using GymProgress.Mobile.Interfaces;
using System.Collections.ObjectModel;

namespace GymProgress.Mobile.ViewModels
{
    public partial class CreateSeanceViewModel : ViewModelBase
    {
        private readonly IExercicesService _exercicesService;
        public CreateSeanceViewModel(IExercicesService exercicesService)
        {
            _exercicesService = exercicesService;
            DisplayExercice();
        }

        [ObservableProperty]
        private string nameSeanceText = string.Empty;

        [ObservableProperty]
        private string errorSeanceText = string.Empty;

        [ObservableProperty]
        private string buttonAddExerciceText = "Ajouter";
        [ObservableProperty]
        private string buttonNextText = "Suivant";
        [ObservableProperty]
        private string buttonValideText = "Valider";

        [ObservableProperty]
        private bool first = true;
        [ObservableProperty]
        private bool second = false;

        [ObservableProperty]
        private bool errorSeance = false;

        [ObservableProperty]
        private bool isSelected;

        [ObservableProperty]
        private ObservableCollection<Exercice> exercices = new();



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

        [RelayCommand]
        private async Task ButtonAddExercice()
        {
            await Shell.Current.GoToAsync($"/{Routes.AddExercicePage}");
        }

        [RelayCommand]
        private async Task ButtonNext()
        {
            if (!string.IsNullOrWhiteSpace(NameSeanceText))
            {
                ErrorSeance = false;
                First = false;
                Second = true;
            }
            else
            {
                var ex = new Exception("Le champs ne peut pas être vide.");
                ErrorSeanceText = ex.Message;
                ErrorSeance = true;
            }
        }

        [RelayCommand]
        private async Task ButtonValide()
        {
            await Shell.Current.GoToAsync($"/{Routes.SeancePage}");
        }
    }
}
