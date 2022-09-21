namespace MauiFirebase.Pages;

public partial class GoogleAuthPage : ContentPage
{
	public GoogleAuthPage(GoogleAuthViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}