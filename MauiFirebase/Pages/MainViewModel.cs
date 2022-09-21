using Plugin.Firebase.Analytics;
using System.Windows.Input;

namespace MauiFirebase.Pages;

public class MainViewModel
{
    private readonly IFirebaseAnalytics? firebaseAnalytics;

    public MainViewModel(IFirebaseAnalytics? firebaseAnalytics = null)
    {
        this.firebaseAnalytics = firebaseAnalytics;
        firebaseAnalytics?.LogEvent(
            "PageOpen",
            new Dictionary<string, object>()
            {
                { "Page", nameof(MainViewModel) } ,
                { "Time", DateTime.UtcNow.ToString() }
            });

        GoToAuthCommand = new Command(GoToAuthCommandExecute);
    }

    public ICommand GoToAuthCommand { get; }

    private async void GoToAuthCommandExecute()
    {
        await Shell.Current.GoToAsync($"/{nameof(AuthPage)}");
    }
}
