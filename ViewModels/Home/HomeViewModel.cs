using CommunityToolkit.Mvvm.ComponentModel;
using GymProgress.Domain.Models;
using System.Collections.ObjectModel;

namespace GymProgress.Mobile.ViewModels.Home
{
    public partial class HomeViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string pseudo = string.Empty;

        [ObservableProperty]
        private string name = string.Empty;

        [ObservableProperty]
        private ObservableCollection<Seance> seances = new();
    }
}
