using EcoRide.Core.Interfaces;
using EcoRide.Core.Models.Entities;
using EcoRide.Data.DbConnecction;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.Data.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly SqlDbConnection _connector;

        public UserRepository()
        {
            //_connection = SqlDbConnection.Instance.Connect();
            _connector = new SqlDbConnection();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            try
            {
                using var conn = _connector.Connect();
                await conn.OpenAsync();
                string query = "SELECT Id, Name, Phone FROM Users WHERE Id = @Id";
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new User(reader.GetString(1), reader.GetString(2))
                    {
                        Id = reader.GetString(0)
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching the user: {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = new List<User>();
            try
            {
                using var conn = _connector.Connect();
                await conn.OpenAsync();
                string query = "SELECT Id, Name, Phone FROM Users";
                using var cmd = new SqlCommand(query, conn);
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    users.Add(new User(reader.GetString(1), reader.GetString(2))
                    {
                        Id = reader.GetString(0)
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching all users: {ex.Message}");
            }
            return users;
        }

        public async Task AddAsync(User user)
        {
            try
            {
                using var conn = _connector.Connect();
                await conn.OpenAsync();
                string query = "INSERT INTO Users (Id, Name, Phone) VALUES (@Id, @Name, @Phone)";
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", user.Id);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Phone", user.Phone);
                await cmd.ExecuteNonQueryAsync();
                Console.WriteLine("User added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while inserting the user: {ex.Message}");
            }
        }

        public async Task DeleteAsync(User user)
        {
            try
            {
                using var conn = _connector.Connect();
                await conn.OpenAsync();
                string query = "DELETE FROM Users WHERE Id = @Id";
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", user.Id);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the user: {ex.Message}");
            }
        }

        public async Task UpdateAsync(User user)
        {
            try
            {
                using var conn = _connector.Connect();
                await conn.OpenAsync();
                string query = "UPDATE Users SET Name = @Name, Phone = @Phone WHERE Id = @Id";
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", user.Id);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Phone", user.Phone);
                await cmd.ExecuteNonQueryAsync();
                Console.WriteLine("User updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the user: {ex.Message}");
            }
        }

        public async Task<bool> ExistsAsync(string id)
        {
            try
            {
                using var conn = _connector.Connect();
                await conn.OpenAsync();
                string query = "SELECT COUNT(1) FROM Users WHERE Id = @Id";
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while checking if the user exists: {ex.Message}");
                return false;
            }
        }
    } 
}
