
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Net.Http.Json;

using System.Text;

using IcecreamApp.Pages;
using IcecreamApp.Services;
using IcecreamApp.Shared.Dtos;

namespace IcecreamApp.ViewModels
{
    public partial class AuthViewModel(IAuthApi authApi) : BaseViewModel
    {
        private readonly IAuthApi _authApi = authApi;



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
        public async Task SignupAsync()
        {
            try
            {
                var user = new SignupRequestDto(Name, Email, Password, Address);
                // Crea il DTO di test
                //var dummyTest = new SignupRequestDto("TestName", "testemail@mail.com", "TestPassword", "TestAddress");

                using (var client = new HttpClient())
                {
                    // Imposta l'indirizzo base dell'API
                    client.BaseAddress = new Uri("http://10.0.2.2:5160"); // o http://localhost:5160 su altre piattaforme

                    // Aggiungi eventuali header necessari
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    var response = await client.PostAsJsonAsync("/api/signup", user);


                    var responseContent = await response.Content.ReadAsStringAsync();


                    Console.WriteLine($"Status Code: {response.StatusCode}");
                    Console.WriteLine($"Response Content: {responseContent}");

                    if (response.IsSuccessStatusCode)
                    {
                        await ShowAlertAsync("Thank for the signup!");
                        await GoToAsync($"//{nameof(HomePage)}", animate: true);
                    }
                    else
                    {
                        await ShowErrorAlertAsync($"something went wrong: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Gestisci eventuali eccezioni
                Console.WriteLine($"Exception: {ex.Message}");
                await ShowErrorAlertAsync("An error occurred during the test API call.");
            }
        }




     [RelayCommand]
        public async Task SigninAsync()
        {
            try
            {
                var user = new SigninRequestDto( Email, Password);
                // Crea il DTO di test
                //var dummyTest = new SignupRequestDto("TestName", "testemail@mail.com", "TestPassword", "TestAddress");

                using (var client = new HttpClient())
                {
                    // Imposta l'indirizzo base dell'API
                    client.BaseAddress = new Uri("http://10.0.2.2:5160"); // o http://localhost:5160 su altre piattaforme

                    // Aggiungi eventuali header necessari
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    var response = await client.PostAsJsonAsync("/api/signin", user);


                    var responseContent = await response.Content.ReadAsStringAsync();


                    Console.WriteLine($"Status Code: {response.StatusCode}");
                    Console.WriteLine($"Response Content: {responseContent}");

                    if (response.IsSuccessStatusCode)
                    {
                        await ShowAlertAsync("Welcome back!");
                        await GoToAsync($"//{nameof(HomePage)}", animate: true);
                    }
                    else
                    {
                        await ShowErrorAlertAsync($"Test API failed with status code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Gestisci eventuali eccezioni
                Console.WriteLine($"Exception: {ex.Message}");
                await ShowErrorAlertAsync("An error occurred during the test API call.");
            }
        }



/*
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

         */


    }
}