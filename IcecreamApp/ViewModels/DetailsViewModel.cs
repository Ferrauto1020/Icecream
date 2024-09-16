using CommunityToolkit.Mvvm.ComponentModel;
using IcecreamApp.Shared.Dtos;
using CommunityToolkit.Mvvm.Input;
using IcecreamApp.Models;
namespace IcecreamApp.ViewModels
{
    //detailspage?Icecream=VALUE
    [QueryProperty(nameof(Icecream), nameof(Icecream))]
    public partial class DetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        private IcecreamDto? _icecream;
        [ObservableProperty]
        private int _quantity;
        [ObservableProperty]
        private IcecreamOption[] _options = [];




        private readonly CartViewModel _cartViewModel;

        public DetailsViewModel(CartViewModel cartViewModel)
        {
            _cartViewModel = cartViewModel;
        }






        partial void OnIcecreamChanged(IcecreamDto? value)
        {
            Options = [];
            if (value is not null)
                return;
            Options = value.Options.Select(o => new IcecreamOption
            {
                Flavor = o.Flavor,
                Topping = o.Topping,
                IsSelected = false
            }).ToArray();
        }

        [RelayCommand]
        private void IncreaseQuantity() => Quantity++;
        [RelayCommand]
        private async void DecreaseQuantity()
        {
            if (Quantity > 0)
                Quantity--;
        }
        [RelayCommand]
        private async Task GoBackAsync() => await Shell.Current.GoToAsync("..", animate: true);

        [RelayCommand]
        private void SelectOption(IcecreamOption newOption)
        {
            var newSelected = !newOption.IsSelected;
            //deselect all the options
            Options = [..Options
            .Select(o=>{
                o.IsSelected = false;
                return o;
            } ).ToArray()];
            newOption.IsSelected = newSelected;
        }





        [RelayCommand]
        private void AddToCart()
        {
            var selectedOption = Options.FirstOrDefault(o => o.IsSelected) ?? Options[0];
            _cartViewModel.AddItemToCart(Icecream, Quantity, selectedOption.Flavor, selectedOption.Topping);
        }


    }
}