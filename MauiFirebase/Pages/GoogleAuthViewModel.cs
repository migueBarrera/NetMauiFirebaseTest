using Plugin.Firebase.Auth;
using System.Windows.Input;

namespace MauiFirebase.Pages
{
    public class GoogleAuthViewModel
    {
        private readonly IFirebaseAuth? firebaseAuth;

        public GoogleAuthViewModel(IFirebaseAuth? firebaseAuth = null)
        {
            this.firebaseAuth = firebaseAuth;

            GoogleAuthCommand = new Command(GoogleAuthCommandExecute);
        }

        public ICommand GoogleAuthCommand { get; set; }

        private async void GoogleAuthCommandExecute()
        {
            try
            {
                if (firebaseAuth != null)
                {
                    var googleUser = await firebaseAuth.LinkWithGoogleAsync();
                    if (googleUser != null)
                    {
                        await Shell.Current.DisplayAlert(
                            "googleUser",
                            $"{googleUser.Email} , {googleUser.DisplayName} , {googleUser}",
                            "OK");
                    }
                    if (firebaseAuth.CurrentUser != null)
                    {
                        await Shell.Current.DisplayAlert(
                            "firebaseAuth.CurrentUser",
                            $"{firebaseAuth.CurrentUser.Email} , {firebaseAuth.CurrentUser.DisplayName}",
                            "OK");
                    }

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
}
