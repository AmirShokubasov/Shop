﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Layers.Models
{
    public class UserVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int RoleId { get; set; }
    }
}