using CommunityToolkit.Mvvm.ComponentModel;
using IcecreamApp.Services;
using IcecreamApp.Shared.Dtos;
using Refit;

namespace IcecreamApp.ViewModels
{
    [QueryProperty(nameof(OrderId), nameof(OrderId))]
    public partial class OrderDetailsViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        private readonly IOrderApi _orderApi;

        public OrderDetailsViewModel(AuthService authService, IOrderApi orderApi)
        {
            _authService = authService;
            _orderApi = orderApi;
        }
        [ObservableProperty]
        private long _orderId;
        [ObservableProperty]
        private OrderItemDto[] _orderItems = [];

        [ObservableProperty]
        private string _title = "Order Items";

        partial  void OnOrderIdChanged(long value)
        {
            Title = $"Order #{value}";
             LoadOrderItemAsync(value);
        }
        private async Task LoadOrderItemAsync(long orderId)
        {
            IsBusy = true;
            try
            {
                OrderItems = await _orderApi.GetOrderItemsAsync(orderId);
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