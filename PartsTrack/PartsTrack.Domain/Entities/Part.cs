
namespace PartsTrack.Domain.Entities
{
    public class Part
    {
        public int PartId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }

        public Part(int partId, string name, string description, decimal price, int stockQuantity)
        {
            PartId = partId;
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
        }

        public void UpdateStock(int quantity)
        {
            StockQuantity += quantity;
        }
    }
}
