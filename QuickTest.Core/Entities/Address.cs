﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Core.Entities
{
    public class Address : IEntity
    {
        public int Id { get; set; }
        public string PostalCode { get; set; }
        public string? Street { get; set; }
        public string? BuildingNumber { get; set; }
        public string PropertyNumber { get; set; }
        public string? City { get; set; }
        public string State { get; set; }
        public string? Country { get; set; }
        public int SchoolId { get; set; }
        public School School { get; set; }

    }
}
