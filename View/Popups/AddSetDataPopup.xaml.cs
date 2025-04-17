using CommunityToolkit.Maui.Views;
using GymProgress.Mobile.ViewModels.Popups;


namespace GymProgress.Mobile.View.Popups;

public partial class AddSetDataPopup : Popup
{
	private readonly AddSetDataPopupViewModel _addSetDataPopupViewModel;
	public AddSetDataPopup(AddSetDataPopupViewModel model)
	{
		InitializeComponent();
		_addSetDataPopupViewModel = model;
		BindingContext = _addSetDataPopupViewModel;
        model.PopupInstance = this;
    }
}