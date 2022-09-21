namespace MauiFirebase.Pages;

public partial class AuthPage : ContentPage
{
	public AuthPage(AuthViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}