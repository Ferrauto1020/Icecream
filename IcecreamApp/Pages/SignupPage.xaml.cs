using CommunityToolkit.Mvvm.Input;
using IcecreamApp.ViewModels;

namespace IcecreamApp.Pages;

public partial class SignupPage : ContentPage
{
    private readonly AuthViewModel authViewModel;

    public SignupPage(AuthViewModel authViewModel)
    {
        InitializeComponent();
        BindingContext = authViewModel;
    }

    private async void SigninLabel_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SigninPage));
    }




}