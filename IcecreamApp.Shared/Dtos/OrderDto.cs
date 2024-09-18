namespace IcecreamApp.Shared.Dtos
{
    public record OrderDto(long Id,DateTime OrderAt, double TotalPrice, int ItemsCount=0 )
    {
        public string ItemsCountDisplay => ItemsCount+ (" "+ (ItemsCount >1? "Items":"Item"));
    };
}