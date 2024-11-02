using Microsoft.Data.SqlClient;
using PartsTrack.Domain.Entities;
using PartsTrack.Domain.Catalogs;
using PartsTrack.Domain.Exceptions;

namespace PartsTrack.Infrastructure.Catalogs
{
    public class InventoryCatalog : IInventoryCatalog
    {
        private readonly string _connectionString;

        public InventoryCatalog(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Inventory GetInventory(int partId, int warehouseId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Inventory WHERE PartId = @PartId AND WarehouseId = @WarehouseId", connection))
                {
                    command.Parameters.AddWithValue("@PartId", partId);
                    command.Parameters.AddWithValue("@WarehouseId", warehouseId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Inventory(
                                inventoryId: (int)reader["InventoryId"],
                                partId: (int)reader["PartId"],
                                warehouseId: (int)reader["WarehouseId"],
                                quantity: (int)reader["Quantity"]
                            );
                        }
                    }
                }
            }
            throw new InventoryNotFoundException(partId, warehouseId);
        }

        // Other inventory-related methods as needed
    }
}
