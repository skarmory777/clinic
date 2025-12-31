using System;

namespace Application.DTOs.Customer
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string? Postcode { get; set; } = string.Empty;
        public string? Notes { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}