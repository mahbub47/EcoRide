using EcoRide.Core.Interfaces;
using EcoRide.Core.Models.Entities;
using EcoRide.Data.DbConnecction;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.Data.Repositories
{
    public class BookingRepository : IRepository<Booking>, IBookingRepository 
    {
        private readonly SqlDbConnection _connector;

        public BookingRepository()
        {
            _connector = SqlDbConnection.Instance;
        }

        public async Task AddAsync(Booking booking)
        {
            try
            {
                using var conn = _connector.Connect();
                conn.Open();
                var query = "INSERT INTO Bookings (Id, UserId, VehicleId, BookingHour, TotalPrice, PaymentStatus) VALUES (@Id, @UserId, @VehicleId, @BookingHour, @TotalPrice, @PaymentStatus)";
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", booking.Id);
                cmd.Parameters.AddWithValue("@UserId", booking.UserId);
                cmd.Parameters.AddWithValue("@VehicleId", booking.VehicleId);
                cmd.Parameters.AddWithValue("@BookingHour", booking.DurationInHours);
                cmd.Parameters.AddWithValue("@TotalPrice", booking.TotalPrice);
                cmd.Parameters.AddWithValue("@PaymentStatus", 0);
                await cmd.ExecuteNonQueryAsync();
                Console.WriteLine("Booking inserted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting booking: {ex.Message}");
            }
        }

        public async Task DeleteAsync(Booking booking)
        {
            try
            {
                using var conn = _connector.Connect();
                await conn.OpenAsync();
                string query = "DELETE FROM Bookings WHERE Id = @Id";
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", booking.Id);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting booking: {ex.Message}");
            }
        }

        public async Task DeleteByVehicle(string vehicleId)
        {
            try
            {
                using var conn = _connector.Connect();
                await conn.OpenAsync();
                string query = "DELETE FROM Bookings WHERE VehicleId = @VehicleId";
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting booking by vehicle: {ex.Message}");
            }
        }

        public async Task<bool> ExistsAsync(string id)
        {
            try
            {
                using var conn = _connector.Connect();
                await conn.OpenAsync();
                string query = "SELECT COUNT(1) FROM Bookings WHERE Id = @Id";
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                var result = await cmd.ExecuteScalarAsync();
                return (int)result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking booking existence: {ex.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            try
            {
                using var conn = _connector.Connect();
                await conn.OpenAsync();
                string query = "SELECT Id, UserId, VehicleId, BookingHour, TotalPrice, PaymentStatus FROM Bookings";
                using var cmd = new SqlCommand(query, conn);
                using var reader = await cmd.ExecuteReaderAsync();
                var bookings = new List<Booking>();
                while (reader.Read()) {
                    bookings.Add(new Booking(reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetDecimal(4), reader.GetBoolean(5)));
                }
                return bookings;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving bookings: {ex.Message}");
                return Enumerable.Empty<Booking>();
            }
        }

        public async Task<Booking> GetByIdAsync(string id)
        {
            try
            {
                using var conn = _connector.Connect();
                await conn.OpenAsync();
                string query = "SELECT Id, UserId, VehicleId, BookingHour, TotalPrice, PaymentStatus FROM Bookings WHERE Id = @Id";
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                using var reader = await cmd.ExecuteReaderAsync();
                if (reader.Read())
                {
                    return new Booking(reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetDecimal(4), reader.GetBoolean(5));
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving booking by ID: {ex.Message}");
                return null;
            }
        }

        public async Task MarkAsPaid(string bookingId)
        {
            try
            {
                using var conn = _connector.Connect();
                await conn.OpenAsync();
                string query = "UPDATE Bookings SET PaymentStatus = 1 WHERE Id = @Id";
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", bookingId);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error marking booking as paid: {ex.Message}");
            }
        }

        public async Task UpdateAsync(Booking booking)
        {
            try
            {
                using var conn = _connector.Connect();
                await conn.OpenAsync();
                string query = "UPDATE Bookings SET UserId = @UserId, VehicleId = @VehicleId, BookingHour = @BookingHour, TotalPrice = @TotalPrice WHERE Id = @Id";
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", booking.Id);
                cmd.Parameters.AddWithValue("@UserId", booking.UserId);
                cmd.Parameters.AddWithValue("@VehicleId", booking.VehicleId);
                cmd.Parameters.AddWithValue("@BookingHour", booking.DurationInHours);
                cmd.Parameters.AddWithValue("@TotalPrice", booking.TotalPrice);
                await cmd.ExecuteNonQueryAsync();
                Console.WriteLine("Booking updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating booking: {ex.Message}");
            }
        }
    }
}
