using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GymProgress.Mobile.ViewModels
{
    public partial class SeanceViewModel : ObservableObject
    {
        public SeanceViewModel()
        {
            VisibleSeance();
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
            await Shell.Current.GoToAsync("CreateSeancePage");
        }

        [RelayCommand]
        private async Task VisibleSeance()
        {
            hasSeance = fake.Any();
            emptySeance = !hasSeance;
        }
    }
}
