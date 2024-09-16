using IcecreamApp.ViewModels;

namespace IcecreamApp.Pages;

public partial class DetailsPage : ContentPage
{
    private readonly DetailsViewModel _detailsViewModel;

    public DetailsPage(DetailsViewModel detailsViewModel)
	{
		InitializeComponent();
        _detailsViewModel = detailsViewModel;
		BindingContext = _detailsViewModel;
    }
}