
using CommunityToolkit.Mvvm.ComponentModel;

namespace IcecreamApp.ViewModels
{
    public partial class AuthViewModel : BaseViewModel
    {

        [ObservableProperty]
        private string? _name;
        [ObservableProperty]
        private string? _email;
        [ObservableProperty]
        private string? _password;


        [ObservableProperty]
        private string? _address;


        public bool CanSignin =>
         !string.IsNullOrWhiteSpace(Email) &&
        !string.IsNullOrWhiteSpace(Password);

        public bool CanSignup =>
        CanSignin &&
        !string.IsNullOrWhiteSpace(Password) &&
        !string.IsNullOrWhiteSpace(Address);

       [RelayCommand]       
        private async Task SignupAsync()
        {
            IsBusy = true;
            try
            {
                var signupDto = new SignupRequestDto(Name,Email,Password,Address);
                //make Api call
            }
            catch (System.Exception)
            {
                
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }


    }
}