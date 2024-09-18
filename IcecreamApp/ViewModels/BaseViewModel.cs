using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Maui.Alerts;
using IcecreamApp.Pages;
using IcecreamApp.Shared.Dtos;
using Refit;
namespace IcecreamApp.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {

        [ObservableProperty]
        private bool _isBusy;

        protected async Task GoToAsync(string url, bool animate = false)
        =>
            await Shell.Current.GoToAsync(url, animate);


        protected async Task GoToAsync(string url, bool animate, IDictionary<string, object> parameters)
        {
            await Shell.Current.GoToAsync(url, animate, parameters);
        }

        protected async Task ShowErrorAlertAsync(string errorMesage) =>
    await Shell.Current.DisplayAlert("Error", errorMesage, "Ok");
        protected async Task ShowAlertAsync(string message) => await ShowAlertAsync("Alert",message);

        protected async Task ShowAlertAsync(string title,string message) =>
    await Shell.Current.DisplayAlert(title, message, "Ok");


        protected async Task ShowToastAsync(string message)
        => await Toast.Make(message).Show();

        protected async Task<bool> ConfirmAsync(string title,string message) => await Shell.Current.DisplayAlert(title,message,"Yes","No");
   

protected async Task HandleApiExceptionAsync(ApiException ex, Action? signout = null)
{
                 if(ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    await ShowErrorAlertAsync("Session expired");
                    signout?.Invoke();
                    await GoToAsync($"//{nameof(OnboardingPage)}");
                }
                Console.WriteLine(ex.Message);
                await ShowErrorAlertAsync(ex.Message);
  
}   
    }
}