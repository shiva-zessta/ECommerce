using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }

        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
