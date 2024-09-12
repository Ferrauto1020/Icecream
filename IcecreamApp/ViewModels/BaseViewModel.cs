using CommunityToolkit.Mvvm.ComponentModel;
using IcecreamApp.Shared.Dtos;
namespace IcecreamApp.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {

        [ObservableProperty]
        private bool _isBusy;
    }
}