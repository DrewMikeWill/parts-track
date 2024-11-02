using Microsoft.Extensions.DependencyInjection;
using PartsTrack.Application;
using PartsTrack.Application.DTOs;
using PartsTrack.Domain.Catalogs;
using PartsTrack.Domain.Services;
using PartsTrack.Infrastructure.Catalogs;
using Microsoft.Extensions.Configuration;

namespace PartsTrack.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Get connection string from configuration
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Setup dependency injection
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IPartCatalog>(provider => new PartCatalog(connectionString))
                .AddSingleton<IInventoryCatalog>(provider => new InventoryCatalog(connectionString))
                .AddSingleton<InventoryService>()
                .AddSingleton<InventoryUseCases>()
                .BuildServiceProvider();

            var inventoryUseCases = serviceProvider.GetService<InventoryUseCases>();

            Console.WriteLine("Welcome to PartsTrack Inventory Console App!");
            while (true)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1 - Add Part");
                Console.WriteLine("2 - Update Part Stock");
                Console.WriteLine("3 - Get Part Details");
                Console.WriteLine("4 - Get Inventory Info");
                Console.WriteLine("0 - Exit");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddPart(inventoryUseCases);
                        break;
                    case "2":
                        UpdatePartStock(inventoryUseCases);
                        break;
                    case "3":
                        GetPartDetails(inventoryUseCases);
                        break;
                    case "4":
                        GetInventoryInfo(inventoryUseCases);
                        break;
                    case "0":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void AddPart(InventoryUseCases inventoryUseCases)
        {
            Console.WriteLine("Enter part name:");
            var name = Console.ReadLine();

            Console.WriteLine("Enter part description:");
            var description = Console.ReadLine();

            Console.WriteLine("Enter part price:");
            if (!decimal.TryParse(Console.ReadLine(), out var price))
            {
                Console.WriteLine("Invalid price. Operation cancelled.");
                return;
            }

            var createPartDTO = new CreatePartDTO(name, description, price);
            inventoryUseCases.AddPart(createPartDTO);
            Console.WriteLine("Part added successfully.");
        }

        static void UpdatePartStock(InventoryUseCases inventoryUseCases)
        {
            Console.WriteLine("Enter part ID:");
            if (!int.TryParse(Console.ReadLine(), out var partId))
            {
                Console.WriteLine("Invalid part ID. Operation cancelled.");
                return;
            }

            Console.WriteLine("Enter quantity to add/subtract:");
            if (!int.TryParse(Console.ReadLine(), out var quantity))
            {
                Console.WriteLine("Invalid quantity. Operation cancelled.");
                return;
            }

            try
            {
                inventoryUseCases.UpdatePartStock(partId, quantity);
                Console.WriteLine("Stock updated successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void GetPartDetails(InventoryUseCases inventoryUseCases)
        {
            Console.WriteLine("Enter part ID:");
            if (!int.TryParse(Console.ReadLine(), out var partId))
            {
                Console.WriteLine("Invalid part ID. Operation cancelled.");
                return;
            }

            try
            {
                var part = inventoryUseCases.GetPartById(partId);
                Console.WriteLine($"Part ID: {part.PartId}");
                Console.WriteLine($"Name: {part.Name}");
                Console.WriteLine($"Description: {part.Description}");
                Console.WriteLine($"Price: {part.Price}");
                Console.WriteLine($"Stock Quantity: {part.StockQuantity}");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void GetInventoryInfo(InventoryUseCases inventoryUseCases)
        {
            Console.WriteLine("Enter part ID:");
            if (!int.TryParse(Console.ReadLine(), out var partId))
            {
                Console.WriteLine("Invalid part ID. Operation cancelled.");
                return;
            }

            Console.WriteLine("Enter warehouse ID:");
            if (!int.TryParse(Console.ReadLine(), out var warehouseId))
            {
                Console.WriteLine("Invalid warehouse ID. Operation cancelled.");
                return;
            }

            try
            {
                var inventory = inventoryUseCases.GetInventoryInfo(partId, warehouseId);
                Console.WriteLine($"Inventory ID: {inventory.InventoryId}");
                Console.WriteLine($"Part ID: {inventory.PartId}");
                Console.WriteLine($"Warehouse ID: {inventory.WarehouseId}");
                Console.WriteLine($"Quantity: {inventory.Quantity}");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
