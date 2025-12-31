using System;
using Domain.Common;
using Domain.Common.Interfaces;

namespace Domain.Entities
{
    public class Customer : BaseEntity, IAggregateRoot
    {
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string? Address { get; private set; }
        public string City { get; private set; }
        public string? Postcode { get; private set; }
        public string? Notes { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Construtor privado para EF Core
        private Customer() { }

        public Customer(
            string fullName,
            string phone,
            string email = null,
            string address = null,
            string city = null,
            string postcode = null,
            string notes = null)
        {
            Validate(fullName, phone, email);

            Id = Guid.NewGuid();
            FullName = fullName;
            Phone = phone;
            Email = email;
            Address = address;
            City = city;
            Postcode = postcode;
            Notes = notes;
            CreatedAt = DateTime.UtcNow;
        }

        private static void Validate(string fullName, string phone, string email)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new DomainException("Full name is required");
            
            if (fullName.Length > 255)
                throw new DomainException("Full name cannot exceed 255 characters");

            if (string.IsNullOrWhiteSpace(phone))
                throw new DomainException("Phone is required");
            
            if (phone.Length > 50)
                throw new DomainException("Phone cannot exceed 50 characters");

            if (!string.IsNullOrWhiteSpace(email) && !IsValidEmail(email))
                throw new DomainException("Invalid email format");

            if (email?.Length > 255)
                throw new DomainException("Email cannot exceed 255 characters");
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public void Update(
            string fullName,
            string phone,
            string email = null,
            string address = null,
            string city = null,
            string postcode = null,
            string notes = null)
        {
            Validate(fullName, phone, email);

            FullName = fullName;
            Phone = phone;
            Email = email;
            Address = address;
            City = city;
            Postcode = postcode;
            Notes = notes;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateEmail(string email)
        {
            if (!string.IsNullOrWhiteSpace(email) && !IsValidEmail(email))
                throw new DomainException("Invalid email format");

            if (email?.Length > 255)
                throw new DomainException("Email cannot exceed 255 characters");

            Email = email;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdatePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                throw new DomainException("Phone is required");
            
            if (phone.Length > 50)
                throw new DomainException("Phone cannot exceed 50 characters");

            Phone = phone;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateAddress(string address, string city, string postcode)
        {
            Address = address;
            City = city;
            Postcode = postcode;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateNotes(string notes)
        {
            Notes = notes;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}