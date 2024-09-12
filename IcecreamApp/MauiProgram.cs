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

		/* 
						builder.Services
						.AddSingleton<HomePage>()
						.AddSingleton<SigninPage>()
						.AddSingleton<SignupPage>(); 
		 */
		builder.Services.AddTransient<AuthViewModel>()
						.AddTransient<SigninPage>()
						.AddTransient<SignupPage>();

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
		.ConfigureHttpClient(httpClient =>
		{
			var baseUrl = DeviceInfo.Platform == DevicePlatform.Android
			? "https://10.0.0.2.2:7263"
			: "https://localhost:7263";
			httpClient.BaseAddress = new Uri(baseUrl);
		});
	}


}
