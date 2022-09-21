namespace MauiFirebase;

using MauiFirebase.Pages;
using Microsoft.Maui.LifecycleEvents;

using Plugin.Firebase.Analytics;

#if IOS
using Plugin.Firebase.iOS;
#elif ANDROID
using Plugin.Firebase.Android;
using Plugin.Firebase.Auth;
using Plugin.Firebase.Shared;
#else
#endif

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .RegisterAppServices()
            .RegisterFirebaseServices()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        Routing.RegisterRoute(nameof(AuthPage), typeof(AuthPage));

        return builder.Build();
	}

    private static MauiAppBuilder RegisterAppServices(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<MainPage>();
        
        builder.Services.AddTransient<AuthViewModel>();
        builder.Services.AddTransient<AuthPage>();
        
        builder.Services.AddTransient<GoogleAuthViewModel>();
        builder.Services.AddTransient<GoogleAuthPage>();

        return builder;
    }


    private static MauiAppBuilder RegisterFirebaseServices(this MauiAppBuilder builder)
    {
        builder.ConfigureLifecycleEvents(events =>
        {
#if IOS
            events.AddiOS(iOS => iOS.FinishedLaunching((app, launchOptions) => {
                    CrossFirebase.Initialize(app, launchOptions, CreateCrossFirebaseSettings());
                    return false;
                }));
#elif ANDROID
            events.AddAndroid(android => android.OnCreate((activity, state) =>
                CrossFirebase.Initialize(activity, state, CreateCrossFirebaseSettings())));
#else
#endif
        });

#if ANDROID || IOS
        builder.Services.AddSingleton(_ => CrossFirebaseAnalytics.Current);
        builder.Services.AddSingleton(_ => CrossFirebaseAuth.Current);
#endif
        return builder;
    }

#if ANDROID || IOS
    private static CrossFirebaseSettings CreateCrossFirebaseSettings()
    {
        return new CrossFirebaseSettings(
            isAnalyticsEnabled: true,
            isAuthEnabled: true);
    }
#endif
}
