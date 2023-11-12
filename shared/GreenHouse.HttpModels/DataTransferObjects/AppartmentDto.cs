namespace GreenHouse.HttpModels.DataTransferObjects
{
    public class AppartmentDto
    {
        public Guid Id { get; init; }
        public string Location { get; set; }
        public int NumberOfGuests { get; set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfSlippingPlaces { get; set; }
        public double Square { get; set; }
        public decimal Bail { get; set; }
        public decimal Price { get; set; }
        public List<string> Photos { get; set; }
        public List<int> Conveniences { get; init; }

        public RulesListDto? Rules { get; set; }
    }
}
