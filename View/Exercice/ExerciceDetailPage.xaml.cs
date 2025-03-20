using GymProgress.Mobile.ViewModels;

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
}