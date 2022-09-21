using MauiFirebase.Pages;

namespace MauiFirebase;

public partial class AuthPage : ContentPage
{
	public AuthPage(AuthViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}