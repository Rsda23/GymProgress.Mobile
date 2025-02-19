using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GymProgress.Mobile.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            VisibleSeance();
        }

        [ObservableProperty]
        private string buttonCreateSeance = "Créer";

        [ObservableProperty]
        private string titleSeance = "Séance";

        [ObservableProperty]
        private bool hasSeance;

        [ObservableProperty]
        private bool emptySeance;

        private List<MainPage> fake = new List<MainPage>();

        [RelayCommand]
        private async Task VisibleSeance()
        {
            hasSeance = fake.Any();
            emptySeance = !hasSeance;
        }
    }
}
