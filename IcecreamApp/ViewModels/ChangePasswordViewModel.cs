using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IcecreamApp.Services;
using IcecreamApp.Shared.Dtos;
using Refit;

namespace IcecreamApp.ViewModels
{
    public partial class ChangePasswordViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        private readonly IAuthApi _authApi;

        public ChangePasswordViewModel(AuthService authService, IAuthApi authApi)
        {
            _authService = authService;
            _authApi = authApi;
        }
        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanChangePassword))]
        private string? _oldPassword;
        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanChangePassword))]
        private string? _newPassword;
        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanChangePassword))]
        private string? _confirmPassword;

        public bool CanChangePassword =>
        !string.IsNullOrWhiteSpace(OldPassword) &&
        !string.IsNullOrWhiteSpace(NewPassword) &&
        !string.IsNullOrWhiteSpace(ConfirmPassword);

        [RelayCommand]
        private async Task ChangePasswordAsync()
        {
            if (NewPassword != ConfirmPassword)
            {
                await ShowErrorAlertAsync("new password and confirm password are not matching");
                return;
            }
            IsBusy = true;
            try
            {
                var dto = new ChangePasswordDto(OldPassword!, NewPassword!);
                var result = await _authApi.ChangePasswordAsync(dto);
                if (!result.IsSuccess)
                {
                    await ShowErrorAlertAsync(result.ErrorMessage);
                    return;
                }
                await ShowAlertAsync("Success", "password changed successfully");
                OldPassword = NewPassword = ConfirmPassword = null;
            }
            catch (ApiException ex)
            {
                await HandleApiExceptionAsync(ex, () => _authService.Signout());
            }
            finally 
            {
                IsBusy = false;
            }

        }
    }
}