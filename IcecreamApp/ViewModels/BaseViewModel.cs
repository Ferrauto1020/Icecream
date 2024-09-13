using CommunityToolkit.Mvvm.ComponentModel;
using IcecreamApp.Pages;
using IcecreamApp.Shared.Dtos;
namespace IcecreamApp.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {

        [ObservableProperty]
        private bool _isBusy;

        protected async Task GoToAsync(string url, bool animate = false)
        =>
            await Shell.Current.GoToAsync(url, animate);
        protected async Task ShowErrorAlertAsync(string errorMesage) =>
    await Shell.Current.DisplayAlert("Error", errorMesage, "Ok");
        protected async Task ShowAlertAsync(string message) =>
    await Shell.Current.DisplayAlert("Hello", message, "Ok");

    }
}