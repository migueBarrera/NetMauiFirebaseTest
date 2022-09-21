using CommunityToolkit.Mvvm.ComponentModel;
using Java.Util;
using Plugin.Firebase.Analytics;
using Plugin.Firebase.Auth;
using System.Windows.Input;

namespace MauiFirebase.Pages;

public class AuthViewModel : ObservableObject
{
    private readonly IFirebaseAnalytics? firebaseAnalytics;
    private readonly IFirebaseAuth? firebaseAuth;

    public AuthViewModel(
        IFirebaseAnalytics? firebaseAnalytics = null,
        IFirebaseAuth? firebaseAuth = null)
    {
        this.firebaseAnalytics = firebaseAnalytics;
        this.firebaseAuth = firebaseAuth;

        firebaseAnalytics?.LogEvent(
            "PageOpen",
            new Dictionary<string, object>()
            {
            { "Page", nameof(AuthViewModel) } ,
            { "Time", DateTime.UtcNow.ToString() }
            });

        SingInCommand = new Command(SingInCommandExecute);
        SingUpCommand = new Command(SingUpCommandExecute);
    }
    public ICommand SingInCommand { get; }

    public ICommand SingUpCommand { get; }

    public string Email { get; set; } = string.Empty;

    public string Pass { get; set; } = string.Empty;

    private async void SingInCommandExecute()
    {
        try
        {
            if (firebaseAuth != null)
            {
                await firebaseAuth.SignInWithEmailAndPasswordAsync(Email, Pass, createsUserAutomatically: false);
                await Shell.Current.DisplayAlert(
                    "",
                    $"{firebaseAuth.CurrentUser.Email} , {firebaseAuth.CurrentUser.DisplayName}",
                    "OK");
            }
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert(
               "Error",
               $"{e.Message}",
               "OK");
        }
    }


    private async void SingUpCommandExecute()
    {
        try
        {
            if (firebaseAuth != null)
            {
                await firebaseAuth.CreateUserAsync(Email, Pass);
                await Shell.Current.DisplayAlert(
                    "",
                    $"{firebaseAuth.CurrentUser.Email} , {firebaseAuth.CurrentUser.DisplayName}",
                    "OK");
            }
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert(
               "Error",
               $"{e.Message}",
               "OK");
        }

    }
}
