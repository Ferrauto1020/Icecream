using IcecreamApp.Pages;

namespace IcecreamApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		RegisterRoutes();
	}

	private readonly static Type[] _routablePageTypes = [
	typeof(SigninPage), 
	typeof(SignupPage),
	typeof(MyOrdersPage),
	typeof(OrderDetailsPage),
	typeof(DetailsPage),

];

	private void RegisterRoutes()
	{
		foreach (var pageType in _routablePageTypes)
		{
			Routing.RegisterRoute(pageType.Name, pageType);
		}
	}

    private async void FlyoutFooter_Tapped(object sender, TappedEventArgs e)
    {
		await Launcher.OpenAsync("https://github.com/Ferrauto1020");
    }

    private async void SignoutMenuItem_Clicked(object sender, EventArgs e)
    {
		await Shell.Current.DisplayAlert("Alert","Signout menu item clicked","Ok");
    }
}
