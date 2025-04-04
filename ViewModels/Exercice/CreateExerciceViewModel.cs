﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GymProgress.Mobile.ViewModels
{
    public partial class CreateExerciceViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string buttonAddExerciceText = "Ajouter";

        [ObservableProperty]
        private string nameExerciceText = string.Empty;

        [ObservableProperty]
        private string buttonValideText = "Valider";

        [RelayCommand]
        private async Task ButtonAddExercice()
        {
            await Shell.Current.GoToAsync("AddExercicePage");
        }

        [RelayCommand]
        private async Task ButtonValide()
        {

        }
    }
}
