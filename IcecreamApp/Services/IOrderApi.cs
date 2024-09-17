using IcecreamApp.Shared.Dtos;
using Refit;

namespace IcecreamApp.Services
{
    [Headers("Authorization: Bearer")]
    public interface IOrderApi
    {
        [Post("/api/order/place-order")]
        Task<ResultDto> PlaceOrderAsync(OrderPlaceDto dto);
    }
}