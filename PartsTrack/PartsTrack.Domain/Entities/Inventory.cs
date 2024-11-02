
namespace PartsTrack.Domain.Entities
{
    public class Inventory
    {
        public int InventoryId { get; private set; }
        public int PartId { get; private set; }
        public int WarehouseId { get; private set; }
        public int Quantity { get; private set; }

        public Inventory(int inventoryId, int partId, int warehouseId, int quantity)
        {
            InventoryId = inventoryId;
            PartId = partId;
            WarehouseId = warehouseId;
            Quantity = quantity;
        }

        public void UpdateQuantity(int quantityChange)
        {
            Quantity += quantityChange;
        }
    }
}
