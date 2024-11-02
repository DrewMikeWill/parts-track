namespace PartsTrack.Application.DTOs
{
    public class PartDTO
    {
        public int PartId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        public PartDTO(int partId, string name, string description, decimal price, int stockQuantity)
        {
            PartId = partId;
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
        }
    }
}
