using IcecreamApp.ViewModels;

namespace IcecreamApp.Pages;

public partial class ProfilePage : ContentPage
{
    private readonly ProfileViewModel _profileViewModel;

    public ProfilePage(ProfileViewModel profileViewModel)
	{
		InitializeComponent();
        _profileViewModel = profileViewModel;
		BindingContext = _profileViewModel;
	}
    protected override void OnAppearing()
    {
		_profileViewModel.Initialize();;
    }
}