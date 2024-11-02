namespace PartsTrack.Application.DTOs
{
    public class WarehouseDTO
    {
        public int WarehouseId { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }

        public WarehouseDTO(int warehouseId, string location, int capacity)
        {
            WarehouseId = warehouseId;
            Location = location;
            Capacity = capacity;
        }
    }
}
