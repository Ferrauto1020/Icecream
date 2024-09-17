using IcecreamApp.ViewModels;

namespace IcecreamApp.Pages;

public partial class CartPage : ContentPage
{
	public CartPage(CartViewModel cartViewModel)
	{
		InitializeComponent();
		BindingContext = cartViewModel;
	}
}