using IcecreamApp.Pages;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

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

		return builder.Build();
	}
}
