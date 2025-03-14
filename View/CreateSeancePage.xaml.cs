using GymProgress.Mobile.ViewModels;

namespace GymProgress.Mobile;

public partial class CreateSeancePage : ContentPage
{
	private readonly CreateSeanceViewModel _createSeanceViewModel;
	public CreateSeancePage(CreateSeanceViewModel model)
	{
		InitializeComponent();
		_createSeanceViewModel = model;
		BindingContext = _createSeanceViewModel;
	}

	//public async void ButtonNext(object sender, EventArgs e)
	//{
	//	NameSeance.IsVisible = false;
 //       await Task.Delay(500);
 //   }
}