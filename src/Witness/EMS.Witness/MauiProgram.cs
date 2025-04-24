using EMS.Witness.Services;
using Core.Application.Services;
using EMS.Witness.Platforms.Services;
using Microsoft.AspNetCore.Components.WebView.Maui;
using System.Reflection;

namespace EMS.Witness;

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
			});

		builder.Services.AddMauiBlazorWebView();
#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif
		builder.Services.AddWitnessServices();

		// https://github.com/dotnet/maui/issues/23390#issuecomment-2202295194
		// Should be fixed in .net9
		WorkaroundBlazorWebViewNotLoadingOnIOS();

		return builder.Build();
	}

	private static void WorkaroundBlazorWebViewNotLoadingOnIOS()
	{
		try
		{
			var handlerType = typeof(BlazorWebViewHandler);
			var field = handlerType.GetField("AppOriginUri", BindingFlags.Static | BindingFlags.NonPublic) ?? throw new Exception("AppOriginUri field not found");
			field.SetValue(null, new Uri("app://localhost/"));
		}
		catch
		{
		}
	}
}
