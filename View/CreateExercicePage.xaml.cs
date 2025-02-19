using GymProgress.Mobile.ViewModels;

namespace GymProgress.Mobile;

public partial class CreateExercicePage : ContentPage
{
	private readonly CreateExerciceViewModel _createExerciceViewModel;
	public CreateExercicePage(CreateExerciceViewModel model)
	{
		InitializeComponent();
		_createExerciceViewModel = model;
		BindingContext = _createExerciceViewModel;
	}
}