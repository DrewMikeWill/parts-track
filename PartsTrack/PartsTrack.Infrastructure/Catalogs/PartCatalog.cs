using Microsoft.Data.SqlClient;
using PartsTrack.Domain.Entities;
using PartsTrack.Domain.Catalogs;

namespace PartsTrack.Infrastructure.Catalogs
{
    public class PartCatalog : IPartCatalog
    {
        private readonly string _connectionString;

        public PartCatalog(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Part GetById(int partId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Parts WHERE PartId = @PartId", connection))
                {
                    command.Parameters.AddWithValue("@PartId", partId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Part(
                                partId: (int)reader["PartId"],
                                name: reader["Name"]?.ToString() ?? string.Empty,
                                description: reader["Description"]?.ToString() ?? string.Empty,
                                price: reader["Price"] is DBNull ? 0 : (decimal)reader["Price"],
                                stockQuantity: reader["StockQuantity"] is DBNull ? 0 : (int)reader["StockQuantity"]
                            );
                        }
                    }
                }
            }

            throw new InvalidOperationException($"No part found with PartId {partId}");
        }

        public void Save(Part part)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("INSERT INTO Parts (Name, Description, Price, StockQuantity) VALUES (@Name, @Description, @Price, @StockQuantity)", connection))
                {
                    command.Parameters.AddWithValue("@Name", part.Name);
                    command.Parameters.AddWithValue("@Description", part.Description);
                    command.Parameters.AddWithValue("@Price", part.Price);
                    command.Parameters.AddWithValue("@StockQuantity", part.StockQuantity);

                    command.ExecuteNonQuery(); // Execute the insert command
                }
            }
        }

        public void Update(Part part)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("UPDATE Parts SET Name = @Name, Description = @Description, Price = @Price, StockQuantity = @StockQuantity WHERE PartId = @PartId", connection))
                {
                    command.Parameters.AddWithValue("@PartId", part.PartId);
                    command.Parameters.AddWithValue("@Name", part.Name);
                    command.Parameters.AddWithValue("@Description", part.Description);
                    command.Parameters.AddWithValue("@Price", part.Price);
                    command.Parameters.AddWithValue("@StockQuantity", part.StockQuantity);

                    command.ExecuteNonQuery(); // Execute the update command
                }
            }
        }

        public void Delete(int partId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM Parts WHERE PartId = @PartId", connection))
                {
                    command.Parameters.AddWithValue("@PartId", partId);
                    command.ExecuteNonQuery(); // Execute the delete command
                }
            }
        }

        public IEnumerable<Part> GetAll()
        {
            var parts = new List<Part>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Parts", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var part = new Part(
                                partId: (int)reader["PartId"],
                                name: reader["Name"]?.ToString() ?? string.Empty,
                                description: reader["Description"]?.ToString() ?? string.Empty,
                                price: reader["Price"] is DBNull ? 0 : (decimal)reader["Price"],
                                stockQuantity: reader["StockQuantity"] is DBNull ? 0 : (int)reader["StockQuantity"]
                            );

                            parts.Add(part);
                        }
                    }
                }
            }

            return parts;
        }
    }
}
