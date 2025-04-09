using GymProgress.Mobile.ViewModels;

namespace GymProgress.Mobile
{
    public partial class SeancePage : ContentPage
    {
        private SeanceViewModel _seanceViewModel;
        public SeancePage(SeanceViewModel model)
        {
            InitializeComponent();
            _seanceViewModel = model;
            BindingContext = _seanceViewModel;
        }

        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            _seanceViewModel.FilterSeancesBySearchTextCommand.Execute(e.NewTextValue);
        }
    }
}
