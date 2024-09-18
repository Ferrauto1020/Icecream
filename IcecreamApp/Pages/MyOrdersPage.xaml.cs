using IcecreamApp.ViewModels;

namespace IcecreamApp.Pages;

public partial class MyOrdersPage : ContentPage
{
    private readonly OrdersViewModel _ordersViewModel;

    public MyOrdersPage(OrdersViewModel ordersViewModel)
	{
		InitializeComponent();
        _ordersViewModel = ordersViewModel;
		BindingContext = _ordersViewModel;
	}

    protected override async void OnAppearing()
    {
		await _ordersViewModel.InitializeAsync();
    }
}