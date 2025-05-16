using GymProgress.Mobile.ViewModels;

namespace GymProgress.Mobile;

public partial class AddExercicePage : ContentPage
{
	private readonly AddExerciceViewModel _addExerciceViewModel;
	public AddExercicePage(AddExerciceViewModel model)
	{
		InitializeComponent();
        _addExerciceViewModel = model;
		BindingContext = _addExerciceViewModel;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = CallAddExercice();
    }

    private async Task CallAddExercice()
    {
        await _addExerciceViewModel.DisplayExercice();
    }
}