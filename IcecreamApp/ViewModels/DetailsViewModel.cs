using CommunityToolkit.Mvvm.ComponentModel;
using IcecreamApp.Shared.Dtos;

namespace IcecreamApp.ViewModels
{
    public partial class DetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        private IcecreamDto? _icecream;
    }
}