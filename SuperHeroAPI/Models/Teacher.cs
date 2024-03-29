﻿using System.Numerics;

namespace SuperHeroAPI.Models
{
    public class Teacher
    {
        public int Id { get; set; }        
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsReady { get; set; }
    }
}