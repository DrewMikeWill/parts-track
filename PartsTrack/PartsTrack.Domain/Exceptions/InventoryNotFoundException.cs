
namespace PartsTrack.Domain.Exceptions
{
    public class InventoryNotFoundException : Exception
    {
        public InventoryNotFoundException(int partId, int warehouseId)
            : base($"Inventory entry not found for Part ID {partId} in Warehouse ID {warehouseId}.")
        {
        }
    }
}
