using System.Collections.ObjectModel;
using IcecreamApp.Models;
using IcecreamApp.Shared.Dtos;

namespace IcecreamApp.ViewModels
{
    public partial class CartViewModel : BaseViewModel
    {
        public ObservableCollection<CartItem> CartItems { get; set; } = [];

       public static int TotalCartCount{get;set;}
       public static event EventHandler<int>? TotalCartCountChanged;

       
        public async void AddItemToCart(IcecreamDto icecream, int quantity, string? flavor = "no flavor", string? topping = "no topping")
        {
            var existingCartItem = CartItems.FirstOrDefault(ci => ci.IcecreamId == icecream.Id);
            if (existingCartItem is not null)
            {
                if (quantity == 0)
                {
                    CartItems.Remove(existingCartItem);
                    await ShowToastAsync("Icecream removed from the cart");
                }
                else
                {
                    existingCartItem.Quantity = quantity;
                    await ShowToastAsync("Quantity updated");
                }
            }
            else
            {
                var CartItem = new CartItem
                {
                    FlavorName = flavor,
                    IcecreamId = icecream.Id,
                    Price = icecream.Price,
                    Quantity = quantity,
                    ToppingName = topping
                };
                CartItems.Add(CartItem);
                await ShowToastAsync("Icecream added to cart");
            }
            TotalCartCount = CartItems.Sum(i=>i.Quantity);
            TotalCartCountChanged?.Invoke(null,TotalCartCount);
        }
    }
}