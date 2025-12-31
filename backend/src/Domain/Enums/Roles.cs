using System.Collections.Generic;

namespace Domain.Enums
{
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public const string Manager = "Manager";
        public const string Veterinarian = "Veterinarian";
        public const string Receptionist = "Receptionist";        
        public static IEnumerable<string> GetAll()
        {
            return new[] { Admin, User, Manager, Veterinarian, Receptionist };
        }
    }
}
