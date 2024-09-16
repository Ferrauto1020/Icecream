using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IcecreamApp.Pages;
using IcecreamApp.Services;
using IcecreamApp.Shared.Dtos;

namespace IcecreamApp.ViewModels
{
    public partial class HomeViewModel(IIcecreamApi icecreamApi, AuthService authService) : BaseViewModel
    {
        private readonly IIcecreamApi _icecreamApi = icecreamApi;
        private readonly AuthService _authService = authService;
        [ObservableProperty]
        private IcecreamDto[] _icecreams = [];

        [ObservableProperty]
        private string _userName = string.Empty;
        private bool _isInitialize;
        public async Task InitializeAsync()
        {
            UserName = _authService.User!.Name;
            if (_isInitialize)
                return;
            IsBusy = true;
            try
            {
                _isInitialize = true;
                Icecreams = await _icecreamApi.GetIcecreamsAsync();
            }
            catch (System.Exception ex)
            {
                _isInitialize = false;
                await ShowErrorAlertAsync(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
        [RelayCommand]
        private async Task GoToDetailsPageAsync(IcecreamDto icecream){
            var parameter = new Dictionary<string, object>
            {
                [nameof(DetailsViewModel.Icecream)]=icecream,
            };
            await GoToAsync(nameof(DetailsPage),animate:true,parameter);
        }
    }
}