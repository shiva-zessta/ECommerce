using ECommerce.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Entities
{
    public class Address
    {
        [Key]
        public int Id { get;  set; }
        
        [ForeignKey(nameof(User))]
        public int UserId { get;  set; }
        public string Street { get;  set; } = null!;
        public string City { get;  set; } = null!;
        public string State { get;  set; } = null!;
        public string Country { get;  set; } = null!;
        public string ZipCode { get;  set; } = null!;
        public AddressType Type { get;  set; } = AddressType.Shipping;
        public bool IsDefault { get;  set; } = false;
        public DateTime CreatedAt { get;  set; }
        public DateTime? UpdatedAt { get; set; } = null!;

        public User User { get;  set; } = null!;
    }
}
