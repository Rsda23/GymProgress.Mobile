using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgress.Mobile.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {

        [ObservableProperty]
        private string buttonCreateSeance = "Créer";

        [ObservableProperty]
        private string titleSeance = "Séance";


    }
}
