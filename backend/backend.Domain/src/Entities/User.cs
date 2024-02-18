using System.Text.Json.Serialization;

namespace backend.Domain.src.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        public string? PostCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public byte[] Salt { get; set; }
        public Role Role { get; set; }

        public List<Order> Orders { get; set; }
        public UserCard UserCard { get; set; }
        public List<ReviewRate> ReviewRates { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        Admin,
        Customer
    }
}