namespace IcecreamApp.Controls;
using CommunityToolkit.Maui.Views;
using IcecreamApp.ViewModels;

public partial class ChangePasswordControl : Popup
{
	public ChangePasswordControl(ChangePasswordViewModel changePasswordViewModel)
	{
		InitializeComponent();
		BindingContext = changePasswordViewModel;
	}

	private async void ClosePopupTab_Tapped(object sender, TappedEventArgs e)
	=>
		await CloseAsync();

}