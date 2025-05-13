using CommunityToolkit.Mvvm.ComponentModel;
using GymProgress.Domain.Models;

namespace GymProgress.Mobile.ViewModels
{
    public partial class ExerciceSelectableViewModel : ObservableObject
    {
        public Exercice Exercice { get; set; } = new();

        [ObservableProperty]
        private bool isSelected;

        public string Nom => Exercice.Nom;

        public string Id => Exercice.ExerciceId;
    }
}
