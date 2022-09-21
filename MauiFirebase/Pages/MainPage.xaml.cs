using MauiFirebase.Pages;

namespace MauiFirebase;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}

