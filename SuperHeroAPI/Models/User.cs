﻿using System.Numerics;

namespace SuperHeroAPI.Models
{
    public class User
    {
        public int Id { get; set; }        
        public string Login { get; set; }        
        public string Password { get; set; }        
        public int RoleId { get; set; }        
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public int GroupId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}