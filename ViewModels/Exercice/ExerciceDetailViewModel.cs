using CommunityToolkit.Mvvm.ComponentModel;
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



        async partial void OnExerciceNomChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                CurrentExercice = await _service.GetExerciceByName(value);
            }
        }
    }
}
