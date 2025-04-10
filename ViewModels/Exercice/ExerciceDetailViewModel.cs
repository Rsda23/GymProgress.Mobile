using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymProgress.Domain.Models;
using GymProgress.Mobile.Core;
using GymProgress.Mobile.Interfaces;

namespace GymProgress.Mobile.ViewModels
{
    [QueryProperty(Constants.TargetProperties.ExerciceNom, Constants.QueryIdentifiers.ExerciceNom)]
    public partial class ExerciceDetailViewModel : ViewModelBase
    {
        private readonly IExercicesService _service;
        public ExerciceDetailViewModel(IExercicesService service)
        {
            _service = service;
        }

        [ObservableProperty]
        private Exercice currentExercice = new();

        [ObservableProperty]
        private string exerciceNom = string.Empty;

        [ObservableProperty]
        private bool confirm = false;


        [RelayCommand]
        private async Task Delete()
        {
            Confirm = await Shell.Current.DisplayAlert(
                "Confirmation",
                "Voulez-vous vraiment supprimer cet exercice ?",
                "Oui", "Non");

            if (Confirm)
            {
                await _service.Delete(currentExercice.ExerciceId);
                await Shell.Current.GoToAsync("..");
            }
        }



        async partial void OnExerciceNomChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                CurrentExercice = await _service.GetExerciceByName(value);
            }
        }
    }
}
