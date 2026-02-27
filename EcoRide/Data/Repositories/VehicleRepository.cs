using EcoRide.Core.Interfaces;
using EcoRide.Core.Models.Base;
using EcoRide.Core.Models.Entities;
using EcoRide.Data.DbConnecction;
using EcoRide.Factory;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.Data.Repositories
{
    public class VehicleRepository : IRepository<Vehicle>, IVehicleRepository
    {
        private readonly SqlDbConnection _connector;

        public VehicleRepository()
        {
            _connector = SqlDbConnection.Instance;
        }

        public async Task<Vehicle> GetByIdAsync(string id)
        {
            try
            {
                using var conn = _connector.Connect();
                await conn.OpenAsync();
                var query = "SELECT Id, PlateNumber, IsAvailable, BasePrice, Type FROM Vehicles WHERE Id = @Id";
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                using var reader = await cmd.ExecuteReaderAsync ();
                if (await reader.ReadAsync())
                {
                    var type = reader.GetString(4);
                    return VehicleFactory.CreateVehicle(type, reader.GetString(0), reader.GetString(1), reader.GetBoolean(2));
                }
                return null;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching vehicle by ID: {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            try
            {
                using var conn = _connector.Connect();
                await conn.OpenAsync();
                var query = "SELECT Id, PlateNumber, IsAvailable, BasePrice, Type FROM Vehicles";
                using var cmd = new SqlCommand(query, conn);
                using var reader = await cmd.ExecuteReaderAsync();
                var vehicles = new List<Vehicle>();
                while (reader.Read())
                {
                    var type = reader.GetString(4);
                    vehicles.Add(VehicleFactory.CreateVehicle(type, reader.GetString(0), reader.GetString(1), reader.GetBoolean(2)));
                }
                return vehicles;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching all vehicles: {ex.Message}");
                return new List<Vehicle>();
            }
        }

        public async Task AddAsync(Vehicle vehicle)
        {
            try
            {
                using var conn = _connector.Connect();
                conn.Open();
                var query = "INSERT INTO Vehicles (Id, PlateNumber, IsAvailable, BasePrice, Type) VALUES (@Id, @PlateNumber, @IsAvailable, @BasePrice, @Type)";
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", vehicle.Id);
                cmd.Parameters.AddWithValue("@PlateNumber", vehicle.PlateNumber);
                cmd.Parameters.AddWithValue("@IsAvailable", vehicle.IsAvailable ? 1 : 0);
                cmd.Parameters.AddWithValue("@BasePrice", vehicle.BasePrice);
                cmd.Parameters.AddWithValue("@Type", vehicle.GetType().Name);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Vehicle inserted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting vehicle: {ex.Message}");
            }
        }

        public async Task DeleteAsync(Vehicle entity)
        {
            try
            {
                using var conn = _connector.Connect();
                await conn.OpenAsync();
                string query = "DELETE FROM Vehicles WHERE Id = @Id";
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", entity.Id);
                await cmd.ExecuteNonQueryAsync();
                Console.WriteLine("Vehicle deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting vehicle: {ex.Message}");
            }
        }

        public async Task UpdateAsync(Vehicle entity)
        {
            try
            {
                using var conn = _connector.Connect();
                await conn.OpenAsync();
                string query = "UPDATE Vehicles SET PlateNumber = @PlateNumber, IsAvailable = @IsAvailable, BasePrice = @BasePrice, Type = @Type WHERE Id = @Id";
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", entity.Id);
                cmd.Parameters.AddWithValue("@PlateNumber", entity.PlateNumber);
                cmd.Parameters.AddWithValue("@IsAvailable", entity.IsAvailable ? 1 : 0);
                cmd.Parameters.AddWithValue("@BasePrice", entity.BasePrice);
                cmd.Parameters.AddWithValue("@Type", entity.GetType().Name);
                await cmd.ExecuteNonQueryAsync();
                Console.WriteLine("Vehicle updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating vehicle: {ex.Message}");
            }
        }

        public async Task<bool> ExistsAsync(string id)
        {
            try
            {
                using var conn = _connector.Connect();
                await conn.OpenAsync();
                string query = "SELECT COUNT(1) FROM Vehicles WHERE Id = @Id";
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                int count = (int)await cmd.ExecuteScalarAsync();
                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if vehicle exists: {ex.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<Vehicle>> GetAvailableVehicle()
        {
            var vehicles = new List<Vehicle>();
            try
            {
                using var conn = _connector.Connect();
                await conn.OpenAsync();
                var query = "SELECT Id, PlateNumber, IsAvailable, BasePrice, Type FROM Vehicles WHERE IsAvailable = 1";
                using var cmd = new SqlCommand(query, conn);
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var type = reader.GetString(4);
                    vehicles.Add(VehicleFactory.CreateVehicle(type, reader.GetString(0), reader.GetString(1), reader.GetBoolean(2)));
                }
                return vehicles;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching available vehicles: {ex.Message}");
            }
            return vehicles;
        }

        public async Task MarkAsBookedAsync(string id)
        {
            try
            {
                using var conn = _connector.Connect();
                await conn.OpenAsync();
                string query = "UPDATE Vehicles SET IsAvailable = 0 WHERE Id = @Id";
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error marking vehicle as booked: {ex.Message}");
            }
        }

        public async Task MarkAsAvailableAsync(string id)
        {
            try
            {
                using var conn = _connector.Connect();
                await conn.OpenAsync();
                string query = "UPDATE Vehicles SET IsAvailable = 1 WHERE Id = @Id";
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error marking vehicle as available: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Vehicle>> GetAvailableVehicleByType(string type)
        {
            try
            {
                using var conn = _connector.Connect();
                await conn.OpenAsync();
                var query = "SELECT Id, PlateNumber, IsAvailable, BasePrice, Type FROM Vehicles WHERE IsAvailable = 1 AND Type = @Type";
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Type", type);
                using var reader = await cmd.ExecuteReaderAsync();
                var vehicles = new List<Vehicle>();
                while (await reader.ReadAsync())
                {
                    vehicles.Add(VehicleFactory.CreateVehicle(reader.GetString(4), reader.GetString(0), reader.GetString(1), reader.GetBoolean(2)));
                }
                return vehicles;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching available vehicles by type: {ex.Message}");
                return new List<Vehicle>();
            }
        }
    }
}
