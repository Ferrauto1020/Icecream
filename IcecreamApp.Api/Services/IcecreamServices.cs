using IcecreamApp.Api.Data;
using IcecreamApp.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace IcecreamApp.Api.Services
{
    public class IcecreamServices(DataContext context)
    {
        private readonly DataContext _context = context;
        public async Task<IcecreamDto[]> GetIcecreamsAsync() =>
        await _context.Icecreams.AsNoTracking()
        .Select(i => new IcecreamDto(
            i.Id,
            i.Name,
            i.Image,
            i.Price,
            i.Options.Select
                (o => new IcecreamOptionDto(o.Flavor, o.Topping))
                .ToArray()
        ))
        .ToArrayAsync();
    }
}