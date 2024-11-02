namespace PartsTrack.Application.DTOs
{
    public class InventoryDTO
    {
        public int InventoryId { get; set; }
        public int PartId { get; set; }
        public int WarehouseId { get; set; }
        public int Quantity { get; set; }

        public InventoryDTO(int inventoryId, int partId, int warehouseId, int quantity)
        {
            InventoryId = inventoryId;
            PartId = partId;
            WarehouseId = warehouseId;
            Quantity = quantity;
        }
    }
}
