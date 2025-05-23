namespace RestaurantReservationSystem.API.DTOs.Requests
{
    public class OrderItemRequest
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}