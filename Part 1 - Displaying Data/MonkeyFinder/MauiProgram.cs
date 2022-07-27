﻿using MonkeyFinder.Services;
using MonkeyFinder.View;

namespace MonkeyFinder;

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

		builder.Services.AddSingleton<IMonkeyNavService, MonkeyNavService>();

		builder.Services.AddSingleton<MonkeyService>();

        builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<MonkeysViewModel>();

		builder.Services.AddTransient<DetailsPage>();		
		builder.Services.AddTransient<MonkeyDetailsViewModel>();        

		return builder.Build();
	}
}
