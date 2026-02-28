using EcoRide.Core.Models.Entities;
using EcoRide.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.Core.Services
{
    public class UserManager
    {
        private readonly UserRepository _userRepository;

        public UserManager(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateUser(string name, string phone)
        {
            var user = new User(Guid.NewGuid().ToString(), name, phone);
            await _userRepository.AddAsync(user);
            var createdUser = await _userRepository.GetByIdAsync(user.Id);
            return createdUser;
        }

        public async Task<User> GetUserById(string userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task<User> UpdateUserInformation(string userId, string name, string phone)
        {
            var user = new User(userId, name, phone);
            await _userRepository.UpdateAsync(user);
            var updatedUser = await _userRepository.GetByIdAsync(userId);
            return updatedUser;
        }

        public async Task DeleteUser(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                await _userRepository.DeleteAsync(user);
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();
            return users.ToList();
        }
    }
}
