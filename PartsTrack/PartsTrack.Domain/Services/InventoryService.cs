using PartsTrack.Domain.Catalogs;
using PartsTrack.Domain.Entities;

namespace PartsTrack.Domain.Services
{
    public class InventoryService
    {
        private readonly IPartCatalog _partCatalog;
        private readonly IInventoryCatalog _inventoryCatalog;

        public InventoryService(IPartCatalog partCatalog, IInventoryCatalog inventoryCatalog)
        {
            _partCatalog = partCatalog;
            _inventoryCatalog = inventoryCatalog;
        }

        public void AddPart(Part part)
        {
            _partCatalog.Save(part);
        }

        public void UpdateStock(int partId, int quantity)
        {
            var part = _partCatalog.GetById(partId);
            part.UpdateStock(quantity);
            _partCatalog.Update(part);
        }

        public Inventory GetInventory(int partId, int warehouseId)
        {
            return _inventoryCatalog.GetInventory(partId, warehouseId);
        }
    }
}
