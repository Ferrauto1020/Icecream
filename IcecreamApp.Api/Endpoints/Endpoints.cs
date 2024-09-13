using IcecreamApp.Shared.Dtos;

namespace IcecreamApp.Api.Services
{
    public static class Endpoints
    {
        public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/signup", async (SignupRequestDto dto, AuthServices authService) =>
            {
                var result = await authService.SignupAsync(dto);
                return TypedResults.Ok(result);
            });

            app.MapPost("/api/signin", async (SigninRequestDto dto, AuthServices authService) =>
            {
               var result =await authService.SigninAsync(dto);
               return  TypedResults.Ok(result);
            });

            return app;
        }
    }
}