using CommunityToolkit.Mvvm.ComponentModel;
using IcecreamApp.Shared.Dtos;
using CommunityToolkit.Mvvm.Input;
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

        [RelayCommand]
        private void IncreaseQuantity() => Quantity++;
        [RelayCommand]
        private async void DecreaseQuantity()
        {
            if (Quantity > 0)
                Quantity--;
        }
        [RelayCommand]
        private async Task GoBackAsync()=> await Shell.Current.GoToAsync("..");
    }
}