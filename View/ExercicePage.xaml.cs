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
}