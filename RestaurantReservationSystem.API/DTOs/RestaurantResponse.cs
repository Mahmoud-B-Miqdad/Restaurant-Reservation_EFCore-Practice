namespace RestaurantReservationSystem.API.DTOs
{
    public class RestaurantResponse
    {
        public int RestaurantId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string OpeningHours { get; set; }
    }
}