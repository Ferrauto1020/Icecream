
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text;

using IcecreamApp.Pages;
using IcecreamApp.Services;
using IcecreamApp.Shared.Dtos;

namespace IcecreamApp.ViewModels
{
    public partial class AuthViewModel(IAuthApi authApi, TestAPi testAPi)  : BaseViewModel
    {
        private readonly IAuthApi _authApi = authApi;
        private readonly TestAPi _testAPi = testAPi;



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
            Console.WriteLine();

                var results = await _testAPi.SignupAsync(signupDto);
            Console.WriteLine(results);

        /* 
                Console.WriteLine($"API Response: Success={results.IsSuccess}, Token={results.Data?.Token}");

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

         */        
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




        [RelayCommand]
        public static async Task TestApiAsync()
        {
            var dummyTest = new SignupRequestDto("Name", "Email@mail", "Password", "Address");

            // Creare manualmente una stringa JSON
            var jsonContent = $"{{\"name\":\"{dummyTest.Name}\",\"email\":\"{dummyTest.Email}\",\"password\":\"{dummyTest.Password}\",\"address\":\"{dummyTest.Address}\"}}";

            var client = new HttpClient { BaseAddress = new Uri("http://10.0.2.2:5160") };
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                Console.WriteLine(client.BaseAddress);
                var response = await client.PostAsync("/api/signup", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response: {responseContent}");
                }
                else
                {
                    Console.WriteLine($"Failed to call API. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("sei dentro l'eccezione");
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}