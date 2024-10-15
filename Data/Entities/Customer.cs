﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Customer : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string PhoneNumber { get; set; } // Telefon
        public string Email { get; set; }
        public ICollection<Order> Orders { get; set; } 
    }
}