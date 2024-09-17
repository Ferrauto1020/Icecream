using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using IcecreamApp.Data;
using IcecreamApp.Models;
using IcecreamApp.Services;
using IcecreamApp.Shared.Dtos;

namespace IcecreamApp.ViewModels
{
    public partial class CartViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;
        public CartViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public ObservableCollection<CartItem> CartItems { get; set; } = [];

        public static int TotalCartCount { get; set; }
        public static event EventHandler<int>? TotalCartCountChanged;


        public async void AddItemToCart(IcecreamDto icecream, int quantity, string? flavor = "no flavor", string? topping = "no topping")
        {
            var existingCartItem = CartItems.FirstOrDefault(ci => ci.IcecreamId == icecream.Id);
            if (existingCartItem is not null)
            {
                var dbCartItem = await _databaseService.GetCartItemAsync(existingCartItem.Id);
                if (quantity == 0)
                {
                    await _databaseService.DeleteCartItemAsync(dbCartItem);
                    CartItems.Remove(existingCartItem);
                    await ShowToastAsync("Icecream removed from the cart");
                }
                else
                {
                    dbCartItem.Quantity = quantity;
                    await _databaseService.UpdateCartItemAsync(dbCartItem);
                    existingCartItem.Quantity = quantity;
                    await ShowToastAsync("Quantity updated");
                }
            }
            else
            {
                var cartItem = new CartItem
                {
                    Name = icecream.Name,
                    FlavorName = flavor,
                    IcecreamId = icecream.Id,
                    Price = icecream.Price,
                    Quantity = quantity,
                    ToppingName = topping
                };
                var entity = new CartItemEntity(cartItem);

                await _databaseService.AddCartItemAsync(entity);
                cartItem.Id = entity.Id;
                CartItems.Add(cartItem);

                await ShowToastAsync("Icecream added to cart");
            }
            NotifyCartCountChanged();
        }

        private void NotifyCartCountChanged()
        {
            TotalCartCount = CartItems.Sum(i => i.Quantity);
            TotalCartCountChanged?.Invoke(null, TotalCartCount);
        }

        public int GetItemCartCount(int icecreamId)
        {
            var existingItem = CartItems.FirstOrDefault(i => i.IcecreamId == icecreamId);
            return existingItem?.Quantity ?? 0;
        }
        public async Task InitializeCartAsync()
        {
            var dbItems = await _databaseService.GetAllItemCartItemsAsync();
            foreach (var dbItem in dbItems)
            {
                CartItems.Add(dbItem.ToCartItemModel());
            }
            NotifyCartCountChanged();
        }
        [RelayCommand]
        private async Task ClearCartAsync()
        {
            if (await ConfirmAsync("Clear Cart?", "Do you really want to clear all the items from the cart?"))
            {
                await _databaseService.ClearCartAsync();
                CartItems.Clear();
                await ShowToastAsync("Cart Cleared");
                NotifyCartCountChanged();
            }
        }
        [RelayCommand]
        private async Task ClearCartItemAsync(int cartItemId)
        {
            if (await ConfirmAsync("Remove Item?", "Do you really want to remove this Icecream?"))
            {
                var existingCartItem = CartItems.FirstOrDefault(i => i.Id == cartItemId);
                if (existingCartItem is null)
                    return;
                CartItems.Remove(existingCartItem);
                
                var dbItem = await _databaseService.GetCartItemAsync(existingCartItem.Id);
                if (dbItem is null)
                    return;
                
                await _databaseService.DeleteCartItemAsync(dbItem);
                await ShowToastAsync("Icecream removed");
                NotifyCartCountChanged();
            }
        }
    }
}