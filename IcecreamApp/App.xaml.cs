using IcecreamApp.Services;
using IcecreamApp.ViewModels;
namespace IcecreamApp;

public partial class App : Application
{
	private readonly CartViewModel _cartViewModel;
	public App(AuthService authService,CartViewModel cartViewModel)
	{
		InitializeComponent();
		_cartViewModel = cartViewModel;
		authService.Initialize();
		MainPage = new AppShell(authService);
	}
	protected override async void OnStart()
	{
		await _cartViewModel.InitializeCartAsync();
	}
}
