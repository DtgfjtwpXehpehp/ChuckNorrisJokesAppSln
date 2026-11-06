using ChuckNorrisJokesApp.ViewModels;

namespace ChuckNorrisJokesApp.Views;

public partial class JokesView : ContentPage
{
	public JokesView(JokesViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}