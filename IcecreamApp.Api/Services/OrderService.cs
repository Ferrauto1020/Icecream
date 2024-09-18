
using IcecreamApp.Api.Data;
using IcecreamApp.Api.Data.Entities;
using IcecreamApp.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace IcecreamApp.Api.Services
{
    public class OrderService
    {
        private readonly DataContext _context;

        public OrderService(DataContext context)
        {
            _context = context;
        }


        public async Task<ResultDto> PlaceOrderAsync(OrderPlaceDto dto, Guid customerId)
        {
            var customer = await _context.Users.FirstOrDefaultAsync(u => u.Id == customerId);
            if (customer is null)
                return ResultDto.Failure("Customer does not exist");
            var orderItems = dto.Items
            .Select(i =>
            new OrderItem
            {
                Flavor = i.Flavor,
                IcecreamId = i.IcecreamId,
                Name = i.Name,
                Price = i.Price,
                Quantity = i.Quantity,
                Topping = i.Topping,
                TotalPrice = i.TotalPrice
            });
            Console.WriteLine(orderItems);
            if (dto.Items == null || !dto.Items.Any())
            {
                Console.WriteLine("Items array is null or empty");
                return ResultDto.Failure("No items in the order");
            }

            var order = new Order
            {
                CustomerId = customerId,
                CustomerAddress = customer.Address,
                CustomerEmail = customer.Email,
                CustomerName = customer.Name,
                OrderAt = DateTime.UtcNow,
                TotalPrice = orderItems.Sum(o => o.TotalPrice),
                Items = orderItems.ToArray()
            };
            Console.WriteLine(order);
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return ResultDto.Success();
            }
            catch (System.Exception ex)
            {
                return ResultDto.Failure(ex.Message);
            }
        }

        public async Task<OrderDto[]> GetUserOrdersAsync(Guid userId) =>
         await _context.Orders.Where(o => o.CustomerId == userId)
            .Select(o => new OrderDto(
                o.Id,
                o.OrderAt,
                o.TotalPrice
            )).ToArrayAsync();

        public async Task<OrderItemDto[]> GetUserOrderItemsAsync(long orderId, Guid userId) =>
        await _context.OrderItems
        .Where(i => i.OrderId == orderId && i.Order.CustomerId == userId)
        .Select(i => new OrderItemDto(
            i.Id,
            i.IcecreamId,
            i.Name,
            i.Quantity,
            i.Price,
            i.Flavor,
            i.Topping
            )).ToArrayAsync();
    }
}