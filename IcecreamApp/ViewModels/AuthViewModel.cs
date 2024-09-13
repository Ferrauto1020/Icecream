
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Net.Http.Json;

using System.Text;

using IcecreamApp.Pages;
using IcecreamApp.Services;
using IcecreamApp.Shared.Dtos;

namespace IcecreamApp.ViewModels
{
    public partial class AuthViewModel(IAuthApi authApi, AuthService authService) : BaseViewModel
    {
        private readonly IAuthApi _authApi = authApi;
        private readonly AuthService _authService = authService;


        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanSignup))]
        private string? _name;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanSignin)), NotifyPropertyChangedFor(nameof(CanSignup))]
        private string? _email;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanSignin)), NotifyPropertyChangedFor(nameof(CanSignup))]
        private string? _password;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanSignup))]
        private string? _address;


        public bool CanSignin =>
         !string.IsNullOrWhiteSpace(Email) &&
        !string.IsNullOrWhiteSpace(Password);

        public bool CanSignup =>
        CanSignin &&
        !string.IsNullOrWhiteSpace(Name) &&
        !string.IsNullOrWhiteSpace(Address);





        [RelayCommand]
        private async Task SignupAsync()
        {
            IsBusy = true;
            try
            {
                var signupDto = new SignupRequestDto(Name, Email, Password, Address);
                //make Api call
                Console.WriteLine(signupDto);
                try
                {
                    var results = await _authApi.SignupAsync(signupDto); Console.WriteLine($"API Response: Success={results.IsSuccess}, Token={results.Data?.Token}");

                    if (results.IsSuccess)
                    {

                        await ShowAlertAsync(results.Data.Token);
                        await GoToAsync($"//{nameof(HomePage)}", animate: true);
                        //navigate to homepage
                    }
                    else
                    {
                        //
                        await ShowErrorAlertAsync(results.ErrorMessage ?? "Unknown error in signin up");
                    }
                }
                catch (System.Exception e)
                {

                    Console.WriteLine(e.Message);
                }


            }
            catch (System.Exception ex)
            {
                await ShowErrorAlertAsync(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }





        [RelayCommand]
        private async Task SigninAsync()
        {
            IsBusy = true;
            try
            {
                var signinDto = new SigninRequestDto(Email, Password);
                //make Api call
                var results = await _authApi.SigninAsync(signinDto);
                if (results.IsSuccess)
                {

                    _authService.Signin(results.Data);
                    await ShowAlertAsync(results.Data.User.Name);
                    await GoToAsync($"//{nameof(HomePage)}", animate: true);
                    //navigate to homepage
                }
                else
                {
                    //
                    await ShowErrorAlertAsync(results.ErrorMessage ?? "Unknown error in signin up");
                }
            }
            catch (System.Exception ex)
            {
                await ShowErrorAlertAsync(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

 
    }
}