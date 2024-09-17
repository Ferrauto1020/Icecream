using IcecreamApp.Pages;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Refit;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using IcecreamApp.Services;
using IcecreamApp.ViewModels;


namespace IcecreamApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			.UseMauiCommunityToolkit()
			;

#if DEBUG
		builder.Logging.AddDebug();
#endif

builder.Services.AddSingleton<DatabaseService>();

		builder.Services.AddTransient<AuthViewModel>()
						.AddTransient<SigninPage>()
						.AddTransient<SignupPage>();

		builder.Services.AddSingleton<AuthService>();
		builder.Services.AddTransient<OnboardingPage>();
		builder.Services.AddSingleton<HomeViewModel>()
						.AddSingleton<HomePage>();
		builder.Services.AddTransient<DetailsViewModel>()
						.AddTransient<DetailsPage>();
		builder.Services.AddSingleton<CartViewModel>();
		ConfigureRefit(builder.Services);
		return builder.Build();
	}

	private static void ConfigureRefit(IServiceCollection services)
	{

		var refitSettings = new RefitSettings
		{
			HttpMessageHandlerFactory = () =>
			{
				//reutrn HttpMessageHandler
				return new HttpClientHandler
				{
					ServerCertificateCustomValidationCallback = (httpRequestMessage, certificate, chain, sslPolicyErrors) =>
				   {
					   return certificate?.Issuer == "CN=localhost" || sslPolicyErrors == SslPolicyErrors.None;
				   }
				};
			}
		};

		services.AddRefitClient<IAuthApi>(refitSettings)
		.ConfigureHttpClient(SetHttpClient);

		services.AddRefitClient<IIcecreamApi>(refitSettings)
		.ConfigureHttpClient(SetHttpClient);

		static void SetHttpClient(HttpClient httpClient)
		{
			var baseUrl = DeviceInfo.Platform == DevicePlatform.Android
			? "http://10.0.2.2:5160"
			: "http://localhost:5160";
			httpClient.BaseAddress = new Uri(baseUrl);

			httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

			Console.WriteLine($"HttpClient BaseAddress: {httpClient.BaseAddress}");
		};
	}

}



