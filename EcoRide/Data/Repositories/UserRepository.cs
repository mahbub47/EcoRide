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
            _connector = SqlDbConnection.Instance;
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
                    return new User(reader["Id"].ToString(), reader["Name"].ToString(), reader["Phone"].ToString())
                    {
                        Id = reader.GetString(0)
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the user.", ex);
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
                    users.Add(new User(reader.GetString(0), reader.GetString(1), reader.GetString(2))
                    {
                        Id = reader.GetString(0)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching all users.", ex);
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
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while inserting the user.", ex);
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
                throw new Exception("An error occurred while deleting the user.", ex);
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
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the user.", ex);
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
                var result = await cmd.ExecuteScalarAsync();
                int count = (int)result;
                return count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while checking if the user exists.", ex);
            }
        }
    } 
}
