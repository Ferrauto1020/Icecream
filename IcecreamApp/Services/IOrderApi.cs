using IcecreamApp.Shared.Dtos;
using Refit;

namespace IcecreamApp.Services
{
    [Headers("Authorization: Bearer")]
    public interface IOrderApi
    {
        [Post("/api/orders/place-order")]
        Task<ResultDto> PlaceOrderAsync(OrderPlaceDto dto);

        [Get("/api/orders")]
        Task<OrderDto[]> GetMyOrdersAsync();
        
        [Get("/api/orders/{orderId}/items")]
        Task<OrderDto[]> GetOrderItemsAsync(long orderId);
    }
}