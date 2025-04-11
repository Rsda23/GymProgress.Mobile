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

        [ObservableProperty]
        private bool isEditing = false;



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

        [RelayCommand]
        private async Task Editing()
        {
            IsEditing = !IsEditing;
        }

        [RelayCommand]
        private async Task SaveEdit()
        {
            IsEditing = false;
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
