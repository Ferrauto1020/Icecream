using IcecreamApp.ViewModels;

namespace IcecreamApp.Pages;

public partial class OrderDetailsPage : ContentPage
{
    private readonly OrderDetailsViewModel _orderDetailsViewModel;

    public OrderDetailsPage(OrderDetailsViewModel orderDetailsViewModel)
	{
		InitializeComponent();
        _orderDetailsViewModel = orderDetailsViewModel;
		BindingContext = _orderDetailsViewModel;
    }
}