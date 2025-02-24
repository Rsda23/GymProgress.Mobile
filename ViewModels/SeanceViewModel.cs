using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymProgress.Mobile.Interfaces;

namespace GymProgress.Mobile.ViewModels
{
    public partial class SeanceViewModel : ViewModelBase
    {
        private readonly ISeancesService _seanceService;

        public SeanceViewModel(ISeancesService seanceService)
        {
            VisibleSeance();
            _seanceService = seanceService;
        }

        [ObservableProperty]
        private string buttonCreateSeanceText = "Créer";

        [ObservableProperty]
        private string titleSeance = "Séance";

        [ObservableProperty]
        private bool hasSeance;

        [ObservableProperty]
        private bool emptySeance;

        private List<SeancePage> fake = new List<SeancePage>();


        [RelayCommand]
        private async Task ButtonCreateSeance()
        {

            var seance = await _seanceService.GetSeanceByName("pull");
            await Shell.Current.GoToAsync($"/{Routes.CreateSeancePage}");
        }

        [RelayCommand]
        private async Task VisibleSeance()
        {
            hasSeance = fake.Any();
            emptySeance = !hasSeance;
        }
    }
}
