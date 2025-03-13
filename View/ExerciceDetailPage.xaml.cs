using GymProgress.Mobile.ViewModels;

namespace GymProgress.Mobile;

public partial class ExerciceDetailPage : ContentPage
{
	private readonly ExerciceViewModel _exerciceDetailPage;
	public ExerciceDetailPage(ExerciceViewModel model)
	{
		InitializeComponent();
		_exerciceDetailPage = model;
		BindingContext = _exerciceDetailPage;
	}
}