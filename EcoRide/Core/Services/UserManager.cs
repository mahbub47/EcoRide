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

        public UserManager()
        {
            _userRepository = new();
        }

        public User CreateUser(string name, string phone)
        {
            var user = new User(name, phone);
            _userRepository.AddAsync(user).Wait();
            return user;
        }

        public User GetUserById(string userId)
        {
            return _userRepository.GetByIdAsync(userId).Result;
        }

        public User UpdateUserInformation(string userId, string name, string phone)
        {
            var user = new User(name, phone)
            {
                Id = userId
            };
            _userRepository.UpdateAsync(user).Wait();
            return user;
        }

        public async void DeleteUser(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                await _userRepository.DeleteAsync(user);
            }
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllAsync().Result.ToList();
        }
    }
}
