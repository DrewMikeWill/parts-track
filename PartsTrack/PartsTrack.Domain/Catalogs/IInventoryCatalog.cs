using PartsTrack.Domain.Entities;

namespace PartsTrack.Domain.Catalogs
{
    public interface IInventoryCatalog
    {
        Inventory GetInventory(int partId, int warehouseId);
        // Other inventory-related methods as needed
    }
}
