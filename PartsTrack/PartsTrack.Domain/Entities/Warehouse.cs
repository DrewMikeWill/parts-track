
namespace PartsTrack.Domain.Entities
{
    public class Warehouse
    {
        public int WarehouseId { get; private set; }
        public string Location { get; private set; }
        public int Capacity { get; private set; }

        public Warehouse(int warehouseId, string location, int capacity)
        {
            WarehouseId = warehouseId;
            Location = location;
            Capacity = capacity;
        }

        public bool CanAccommodate(int quantity)
        {
            // Placeholder logic to check if the warehouse has enough capacity for a new quantity.
            // Implementation may vary depending on how you track current inventory in this warehouse.
            return Capacity >= quantity;
        }

        public void UpdateCapacity(int newCapacity)
        {
            Capacity = newCapacity;
        }
    }
}
