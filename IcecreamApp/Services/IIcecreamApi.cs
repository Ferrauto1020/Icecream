using IcecreamApp.Shared.Dtos;
using Refit;

namespace IcecreamApp.Services
{
    public interface IIcecreamApi
    {
        [Get("/api/icecreams")]
        Task<IcecreamDto[]> GetIcecreamsAsync();
    }
}