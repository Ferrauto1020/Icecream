using CommunityToolkit.Mvvm.ComponentModel;

namespace IcecreamApp.Models
{
    public partial class IcecreamOption :ObservableObject
    {
        public string Flavor {get;set;}
        public string Topping {get;set;}
        [ObservableProperty]
        public bool _isSelected;
    }
}