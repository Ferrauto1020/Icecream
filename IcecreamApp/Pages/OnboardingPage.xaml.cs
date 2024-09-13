using IcecreamApp.Services;

namespace IcecreamApp.Pages;

public partial class OnboardingPage : ContentPage
{
	private readonly AuthService _authService;

	public OnboardingPage(AuthService authService)
	{
		InitializeComponent();
		_authService = authService;
	}

	protected async override void OnAppearing()
	{
		if (_authService.User is not null
		&& _authService.User.Id != default(Guid)
		&& !string.IsNullOrWhiteSpace(_authService.Token))
		{
			//user already logged in
			await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
		}

	}


	private async void Signin_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(SigninPage));
	}
	private async void Signup_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(SignupPage));
	}
}