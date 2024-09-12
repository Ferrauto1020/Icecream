using IcecreamApp.Shared.Dtos;

namespace IcecreamApp.Api.Services
{
    public static class Endpoints
    {
        public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/signup", async (SignupRequestDto dto, AuthServices authService) =>
            {
                TypedResults.Ok(await authService.SignupAsync(dto));
            });

            app.MapPost("/api/signin", async (SigninRequestDto dto, AuthServices authService) =>
            {
                TypedResults.Ok(await authService.SigninAsync(dto));
            });

            return app;
        }
    }
}