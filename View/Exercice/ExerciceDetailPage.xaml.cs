using GymProgress.Mobile.ViewModels;
using GymProgress.Mobile.ViewModels.Popups;

namespace GymProgress.Mobile;

public partial class ExerciceDetailPage : ContentPage
{
	private readonly ExerciceDetailViewModel _exerciceDetailPage;
    public ExerciceDetailPage(ExerciceDetailViewModel model)
	{
		InitializeComponent();
		_exerciceDetailPage = model;
		BindingContext = _exerciceDetailPage;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _exerciceDetailPage.DisplaySetData();
    }
}