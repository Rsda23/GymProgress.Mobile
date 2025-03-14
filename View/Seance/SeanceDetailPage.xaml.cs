using GymProgress.Mobile.ViewModels;

namespace GymProgress.Mobile;

public partial class SeanceDetailPage : ContentPage
{
	private readonly SeanceDetailViewModel _seanceDetailViewModel;
	public SeanceDetailPage(SeanceDetailViewModel model)
	{
		InitializeComponent();
		_seanceDetailViewModel = model;
		BindingContext = _seanceDetailViewModel;
	}
}