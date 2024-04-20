namespace E_CommerceAPI.Application.Features.Queries.Basket.GetBasketItems
{
    public class GetBasketITemsQueryResponse
    {
        public string BasketItemId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quatity { get; set; }
    }
}