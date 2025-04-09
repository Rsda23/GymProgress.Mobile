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
        private readonly ISeancesService _seancesService;
        public CreateSeanceViewModel(IExercicesService exercicesService, ISeancesService seancesService)
        {
            _exercicesService = exercicesService;
            _seancesService = seancesService;
        }

        [ObservableProperty]
        private string nameSeanceText = string.Empty;

        [ObservableProperty]
        private string errorSeanceText = string.Empty;

        [ObservableProperty]
        private bool errorSeance = false;



        [RelayCommand]
        private async Task ButtonValide()
        {
            if (!string.IsNullOrWhiteSpace(NameSeanceText))
            {
                ErrorSeance = false;
                await _seancesService.PostSeance(NameSeanceText);
                await Shell.Current.GoToAsync($"///{Routes.SeancePage}");
            }
            else
            {
                var ex = new Exception("Le champs ne peut pas être vide.");
                ErrorSeanceText = ex.Message;
                ErrorSeance = true;
            }
        }
    }
}
