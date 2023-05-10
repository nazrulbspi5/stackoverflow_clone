﻿namespace StackOverflow.Services.BusinessObjects.Authentication
{
    public class ApplicationUser
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; }
        public string Password { get; set; } = null!;
        public bool RememberMe { get; set; } 
       
    }
}
