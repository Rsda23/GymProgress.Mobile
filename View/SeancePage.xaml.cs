using GymProgress.Mobile.ViewModels;

namespace GymProgress.Mobile
{
    public partial class SeancePage : ContentPage
    {
        private SeanceViewModel _SeanceViewModel;
        public SeancePage(SeanceViewModel model)
        {
            InitializeComponent();
            _SeanceViewModel = model;
            BindingContext = _SeanceViewModel;
        }
    }

}
