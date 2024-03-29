﻿using QuickTest.Application.Groups;
using QuickTest.Application.Users.UserRole;
using QuickTest.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Users.CreateUser
{
    public class CreateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public UserRoleDto UserRole { get; set; }
        public GroupDto Group { get; set; }
    }
}
