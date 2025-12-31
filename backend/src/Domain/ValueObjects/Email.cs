using System;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public record Email
    {
        public string Value { get; }

        private Email(string value) => Value = value;

        public static Email Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email addresses cannot be empty.");

            if (!IsValidEmail(email))
                throw new ArgumentException("Invalid email address");

            return new Email(email.ToLower());
        }

        private static bool IsValidEmail(string email)
        {
            var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        public override string ToString() => Value;
    }
}