using GymProgress.Mobile.ViewModels;

namespace GymProgress.Mobile;

public partial class ExercicePage : ContentPage
{
	private readonly ExerciceViewModel _exerciceViewModel;
	public ExercicePage(ExerciceViewModel model)
	{
		InitializeComponent();
		_exerciceViewModel = model;
		BindingContext = _exerciceViewModel;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _exerciceViewModel.DisplayExercice();
    }

    private void OnSearchBar(object sender, TextChangedEventArgs e)
    {
        _exerciceViewModel.FilterExercicesBySearchCommand.Execute(e.NewTextValue);
    }
}