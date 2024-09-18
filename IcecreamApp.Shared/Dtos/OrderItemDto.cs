namespace IcecreamApp.Shared.Dtos
{
    public record OrderItemDto(long Id,int IcecreamId, string Name, int Quantity,double Price, string Flavor, string Topping)
    {

        public double TotalPrice => Quantity*Price;
    }
}