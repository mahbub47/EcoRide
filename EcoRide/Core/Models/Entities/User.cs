using System;
using System.Collections.Generic;
using System.Text;

namespace EcoRide.Core.Models.Entities
{
    public class User
    {
        public string Id { get; init; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public User(string id, string name, string phone)
        {
            Id = id;
            Name = name;
            Phone = phone;
        }
    }
}
