namespace IcecreamApp.Shared.Dtos
{
    public record OrderPlaceDto(OrderDto Order,OrderItemDto[] Items );
}