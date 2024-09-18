using System.Security.Claims;
using IcecreamApp.Shared.Dtos;

namespace IcecreamApp.Api.Services
{
    public static class Endpoints
    {
        private static Guid GetUserId(this ClaimsPrincipal principal) =>
        Guid.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier)!);



        public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/signup", async (SignupRequestDto dto, AuthServices authService) =>
            {
                var result = await authService.SignupAsync(dto);
                return TypedResults.Ok(result);
            });

            app.MapPost("/api/signin", async (SigninRequestDto dto, AuthServices authService) =>
            {
                var result = await authService.SigninAsync(dto);
                return TypedResults.Ok(result);
            });

            app.MapGet("/api/icecreams",
            async (IcecreamServices icecreamService) =>
            {
                var result = await icecreamService.GetIcecreamsAsync();
                return TypedResults.Ok(result);
            });
            var orderGroup = app.MapGroup("/api/orders").RequireAuthorization();
            orderGroup.MapPost("/place-order",
            async (OrderPlaceDto dto, ClaimsPrincipal principal, OrderService orderService) =>

                {
                    var result = await orderService.PlaceOrderAsync(dto, principal.GetUserId());
                    return result;
                }
                );
            orderGroup.MapGet("",
            async (ClaimsPrincipal principal, OrderService orderService) =>

                TypedResults.Ok(
                    await orderService.GetUserOrdersAsync(principal.GetUserId())
                    )
                );
            orderGroup.MapGet("/{orderId:long}/items",
                async(long orderId, ClaimsPrincipal principal, OrderService orderService) =>
                TypedResults.Ok(
                    await orderService.GetUserOrderItemsAsync(orderId,principal.GetUserId())
                )
            );

            return app;
        }
    }
}